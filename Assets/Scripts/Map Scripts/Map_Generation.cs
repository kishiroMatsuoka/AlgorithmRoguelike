using System.Collections.Generic;
using UnityEngine;
using Map;

public class Map_Generation : MonoBehaviour
{
    public int number_of_nodes = 0;
    [SerializeField] private Map_Configuration configuration;
    [SerializeField] private GameObject Empty;
    [SerializeField] private GameObject[] icons;
    [SerializeField] private GameObject[] enemylist,elitelist, bosses;
    public float ChanceUpgrade, ChanceHeal,ChanceChest;
    private List<Node> nodes = new List<Node>();
    private int n_bosses=0, n_stores=0, n_elites=0;
    private bool BossNodeCreated = false;
    private GameObject bossnode;
    private List<Vector3> coords = new List<Vector3>();
    private float max_x = 0f;

    private void Start()
    {
        TestAlg();
    }
    public void TestAlg()
    {
        nodes.Clear();
        GenerateMapRecursive(0);
        SpawnMap();
        GetComponent<MapInteractions>().nodes = nodes;
        nodes.Find(x => x.Layer == 0).name = "A";
        GetComponent<MapInteractions>().RenameSimple(configuration.Map_Layers);
    }
    private void SpawnMap()
    {
        Node spawn = nodes.Find(x => x.Layer == 0);
        var nodespawn = Instantiate(spawn.icon, gameObject.transform);
        nodespawn.transform.position = Vector3.zero;
        nodespawn.GetComponent<NodeContainer>().SetNode(spawn);
        coords.Clear();
        SpawnChild(spawn, nodespawn);
        SceneControl sc = GameObject.Find("EventSystem").GetComponent<SceneControl>();
        sc.CurrentNode = spawn;
        sc.CNodeObject = nodespawn.transform;
        sc.MaxLvlZone = configuration.Map_MaxLevel;

    }
    
    private Node GenerateMapRecursive(int layer)
    {
        if (layer < configuration.Map_Layers)
        {
            Node node = GenerateNode(layer);
            number_of_nodes++;
            int temp = Random.Range(1, configuration.Map_Complexity);//lvl 1 - 1min, max3
            //exclusive 3 -> 1,2
            if(layer == 0)
            {
                temp = configuration.Map_Complexity;
            }
            for (int i = 1; i <= temp; i++)
            {
                node.AddConection(GenerateMapRecursive(layer + 1), configuration);
            }
            nodes.Add(node);
            return node;
        }
        else if (layer == configuration.Map_Layers)
        {
            Node node = GenerateNode(layer);
            number_of_nodes++;
            node.AddConection(GenerateMapRecursive(layer + 1), configuration);
            nodes.Add(node);
            return node;
        }
        else
        {
            if (BossNodeCreated)
            {
                return nodes.Find(x => x.N_Type == NodeType.Boss);
            }
            else
            {
                //Debug.Log("Boss is created");
                BossNodeCreated = true;
                Node bossnode = GenerateNode(layer);
                number_of_nodes++;
                nodes.Add(bossnode);
                return bossnode;
            }
        }
    }
    private void SpawnChildRandom(Node parent, GameObject parent_t)
    {
        float y, x,x_parent = parent_t.transform.position.x, y_parent = parent_t.transform.position.y;
        int childs = parent.Node_Conections.Count, count = 0;
        //lines
        LineRenderUpdate[] lines = new LineRenderUpdate[childs];
        for (int i = 0; i < childs; i++)
        {
            var temp = Instantiate(Empty, parent_t.transform);
            temp.transform.SetAsFirstSibling();
            temp.GetComponent<LineRenderer>().SetPosition(0, parent_t.transform.position);
            lines[i] = temp.GetComponent<LineRenderUpdate>();

        }
        //recursive
        foreach (Node_Conection nc in parent.Node_Conections)
        {
            if (nc.Conection_Child.N_Type == NodeType.Boss)
            {
                if (bossnode != null)
                {
                    lines[count].SetReference(bossnode.transform);
                    count++;
                }
                else
                {
                    var nodeimage = Instantiate(nc.Conection_Child.icon);
                    x = max_x + 5f;
                    x = Mathf.Round(x * 1.0f) * 1.0f;
                    nodeimage.transform.position = new Vector3(x, 0f, 0f);
                    nodeimage.GetComponent<NodeContainer>().SetNode(nc.Conection_Child);
                    lines[count].SetReference(nodeimage.transform);
                    count++;
                    bossnode = nodeimage;
                }
            }
            else
            {
                x = x_parent + 3f;
                x = Mathf.Round(Random.Range(x, x + 1f) * 100.0f) * 0.01f;
                y = Random.Range(y_parent - 3.0f, y_parent + 3.0f);
                int stop = 0;
                while (true)
                {
                    if (Physics.OverlapSphere(new Vector3(x, y, 0f), 0.6f, 1 << 7).Length > 0)
                    {
                        Debug.Log("Detecting collisions : " + Physics.OverlapSphere(new Vector3(x, y, 0f), 0.6f, 1 << 7).Length);
                        foreach (var test in Physics.OverlapSphere(new Vector3(x, y, 0f), 0.55f, 1 << 7))
                        {
                            Debug.Log(test.gameObject.name + "\t coor : " + test.transform.position);
                        }
                        y = Mathf.Round(Random.Range(y_parent - 3.0f, y_parent + 3.0f) * 100.0f) * 0.01f;
                        x = Mathf.Round(Random.Range(x, x+3f) * 100.0f) * 0.01f;
                        stop++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (stop > 0)
                {
                    Debug.Log("Times coords updated : " + stop + "\t Final coord : " + new Vector3(x, y, 0f));
                }
                var nodeimage = Instantiate(nc.Conection_Child.icon);
                nodeimage.GetComponent<NodeContainer>().SetNode(nc.Conection_Child);
                nodeimage.transform.position = new Vector3(x, y, 0f);
                lines[count].SetReference(nodeimage.transform);
                count++;
                if(nodeimage.transform.position.x > max_x)
                {
                    max_x = nodeimage.transform.position.x;
                }
                SpawnChildRandom(nc.Conection_Child, nodeimage);
            }
        }
    }
    private void SpawnChild(Node parent, GameObject parent_t)
    {
        float y, x,y_parent = parent_t.transform.position.y;
        int childs = parent.Node_Conections.Count, count = 0;
        //lines
        LineRenderUpdate[] lines = new LineRenderUpdate[childs];
        for (int i=0; i < childs; i++)
        {
            var temp = Instantiate(Empty, parent_t.transform);
            temp.transform.SetAsFirstSibling();
            temp.GetComponent<LineRenderer>().SetPosition(0, parent_t.transform.position);
            lines[i] = temp.GetComponent<LineRenderUpdate>();
            
        }
        //recursive
        foreach (Node_Conection nc in parent.Node_Conections)
        {
            if(nc.Conection_Child.N_Type == NodeType.Boss)
            {
                if(bossnode != null)
                {
                    lines[count].SetReference(bossnode.transform);
                    count++;
                }
                else
                {
                    var nodeimage = Instantiate(nc.Conection_Child.icon, gameObject.transform);
                    x = (nc.Conection_Child.Layer * 3f) + 1.5f;
                    x = Mathf.Round(x * 1.0f) * 1.0f;
                    nodeimage.transform.position = new Vector3(x, 0f, 0f);
                    nodeimage.GetComponent<NodeContainer>().SetNode(nc.Conection_Child);
                    lines[count].SetReference(nodeimage.transform);
                    count++;
                    bossnode = nodeimage;
                }
            }
            else
            {
                if (childs > 2)
                {
                    float offset = 6f / (childs - 1);
                    y = y_parent - 3f + offset * (count + 1);
                    y = Mathf.Round(y * 100.0f) * 0.01f;
                }
                else if (childs == 2)
                {
                    if (count == 0)
                    {
                        y = y_parent - 1.75f;
                        y = Mathf.Round(y * 100.0f) * 0.01f;
                    }
                    else
                    {
                        y = y_parent + 1.75f;
                        y = Mathf.Round(y * 100.0f) * 0.01f;
                    }
                }
                else//1 nodo
                {
                    y = y_parent;
                }
                x = nc.Conection_Child.Layer * 3f;
                x = Mathf.Round(x * 1.0f) * 1.0f;
                float xref = x;
                int stop = 0;
                while (true)
                {
                    Physics2D.SyncTransforms();
                    if (Physics2D.OverlapCircleAll(new Vector3(x, y, 0f), 0.55f, 1 << 7 ).Length > 0)
                    {
                        //Debug.Log("collisions detected in : " + new Vector3(x, y, 0f));
                        foreach (var test in Physics2D.OverlapCircleAll(new Vector3(x, y, 0f), 0.55f, 1 << 7))
                        {
                            //Debug.Log("Object: "+test.gameObject.name + "\t coor : " + test.transform.position);
                        }
                        y = Random.Range(y_parent - 3.0f, y_parent + 3.0f);
                        x = Random.Range(xref - 1.6f, xref + 1.6f);
                        stop++;
                    }
                    else
                    {
                        //Debug.Log("No collision Coor: " + new Vector3(x, y, 0f));
                        break;
                    }
                }
                if(stop > 0)
                {
                    //Debug.Log("Times coords updated : " + stop+"\t Final coord : "+ new Vector3(x, y, 0f));
                }
                var nodeimage = Instantiate(nc.Conection_Child.icon, gameObject.transform);
                nodeimage.GetComponent<NodeContainer>().SetNode(nc.Conection_Child);
                nodeimage.transform.position = new Vector3(x, y, 0f);
                coords.Add(new Vector3(x, y, 0f));
                lines[count].SetReference(nodeimage.transform);
                count++;
                SpawnChild(nc.Conection_Child, nodeimage);
            }
        }
    }
    private Node GenerateNode(int layer)
    {
        Node node = new Node();
        node.Layer = layer;
        if (layer == 0)
        {
            
            node.N_Type = NodeType.Spawn;
            node.icon = icons[0];
            node.name = icons[0].name;
        }
        else if (layer > configuration.Map_Layers)
        {
            node.N_Type = NodeType.Boss;
            node.icon = icons[4];
            node.name = icons[4].name;
            node.Node_Enemies.Add(bosses[Random.Range(0, bosses.Length)]);
            for (int x = 0; x < 2; x++)
            {
                node.Node_Enemies.Add(enemylist[Random.Range(0, enemylist.Length)]);
            }
        }
        else
        {
            NonSpecialNode(node);
        }
        return node;
    }
    private Node NonSpecialNode(Node node)
    {
        int temp = Random.Range(0, 9);
        float chance;
        switch (temp)
        {
            case 0:
                node.N_Type = NodeType.Normal;
                node.icon = icons[1];
                node.name = icons[1].name;
                break;
            case 1:
                node.N_Type = NodeType.Combat;//combat
                node.icon = icons[2];
                node.name = icons[2].name;

                for (int x = 0; x < 3; x++)
                {
                    int re = Random.Range(0, enemylist.Length);
                    node.Node_Enemies.Add(enemylist[re]);
                }
                break;
            case 2://elites
                if (n_elites < configuration.Map_Elites)
                {
                    node.N_Type = NodeType.Elite;//elite
                    node.icon = icons[3];
                    node.name = icons[3].name;
                    for (int x = 0; x < 3; x++)
                    {
                        if (Random.Range(0, 2) > 0)
                        {
                            int re = Random.Range(0, elitelist.Length);
                            node.Node_Enemies.Add(enemylist[re]);
                        }
                        else
                        {
                            int re = Random.Range(0, enemylist.Length);
                            node.Node_Enemies.Add(enemylist[re]);
                        }
                        
                    }
                    n_elites++;
                }
                else
                {
                    NonSpecialNode(node);
                }
                break;
            case 3://heal
                chance = Random.value;
                if (chance < ChanceHeal)
                {
                    node.N_Type = NodeType.Heal;
                    node.icon = icons[9];
                    node.name = icons[9].name;
                }
                else
                {
                    NonSpecialNode(node);
                }
                break;
            case 4://upgrade
                chance = Random.value;
                if (chance < ChanceUpgrade)
                {
                    node.N_Type = NodeType.Upgrade;
                    node.icon = icons[8];
                    node.name = icons[8].name;
                }
                else
                {
                    NonSpecialNode(node);
                }
                break;
            case 5://stores
                if (n_stores < configuration.Map_Stores)
                {
                    node.N_Type = NodeType.Store;
                    node.icon = icons[6];
                    node.name = icons[6].name;
                    n_stores++;
                }
                else
                {
                    NonSpecialNode(node);
                }
                break;
            case 6://chest
                chance = Random.value;
                if (chance < ChanceChest)
                {
                    node.N_Type = NodeType.Chest;
                    node.icon = icons[7];
                    node.name = icons[7].name;
                }
                else
                {
                    NonSpecialNode(node);
                }
                break;
            case 7://random node
                node.N_Type = NodeType.Random;
                node.icon = icons[5];
                node.name = icons[5].name;
                break;
            case 8://blessing
                chance = Random.value;
                if (chance < -1f)
                {
                    node.N_Type = NodeType.Blessing;
                    node.icon = icons[10];
                    node.name = icons[10].name;
                }
                else
                {
                    NonSpecialNode(node);
                }
                break;
        }
        return node;
    }
}



