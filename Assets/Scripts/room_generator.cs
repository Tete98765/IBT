using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class room_generator : MonoBehaviour
{

    public class cell
    {
        public bool visited = false;
        public room room_on_cell;
    }

    public class room {
        int width;
        int height;
        private GameObject rooms;
        private Vector3 room_positions;
        private Vector2Int room_sizes;
        private List<Vector3> floors_positions = new List<Vector3>();
        private List<Vector3> walls_positions = new List<Vector3>();
        private List<string> walls_directions = new List<string>();
        private List<GameObject> walls_prefab = new List<GameObject>();
        private List<Vector3> doors_position = new List<Vector3>();

        public room(int w, int h, Vector3 pos, Vector2Int size, GameObject room_obj, List<Vector3> walls_pos, List<string> walls_dir, List<GameObject> wall_pref, List<Vector3> floors_pos) {
            width = w;
            height = h;
            room_sizes = size;
            room_positions = pos;
            rooms = room_obj;
            walls_positions = walls_pos;
            walls_directions = walls_dir;
            walls_prefab = wall_pref;
            floors_positions = floors_pos;
        }

        public float get_width() {
            return width;
        }

        public float get_height() {
            return height;
        }

        public Vector3 get_position() {
            return room_positions;
        }

        public Vector2Int get_size() {
            return room_sizes;
        }

        public void destroy_wall(Vector3 pos, string dir) {
            doors_position.Add(pos);
            for(int i = 0; i < walls_positions.Count; i++) {
                if(walls_positions[i] == pos) {
                    if(walls_directions[i] == dir) {
                        Destroy(walls_prefab[i]);
                        walls_prefab.RemoveAt(i);
                        walls_positions.RemoveAt(i);
                        walls_directions.RemoveAt(i);
                        break;
                    }
                    
                }
            }
        }

        public Vector3 get_floor_position() {
            Vector3 spawn_floor = floors_positions[Random.Range(0, floors_positions.Count)];
            return spawn_floor;
        }

        public Vector3 get_random_wall(int num) {
            Vector3 spawn_wall = walls_positions[num];
            return spawn_wall;
        }

        public string get_random_wall_direction(int num) {
            return walls_directions[num];
        }

        public List<Vector3> get_walls_position() {
            return walls_positions;
        }

        public List<string> get_walls_direction() {
            return walls_directions;
        }

        public bool check_door(Vector3 floor_pos) {
            bool is_door = true;
            for(int i = 0; i < doors_position.Count; i++) {
                if(doors_position[i] == floor_pos) {
                    is_door = false;
                }
            }

            return is_door;
        }

        public bool check_wall(Vector3 floor_pos) {
            bool is_wall = true;
            for(int i = 0; i < walls_positions.Count; i++) {
                if(walls_positions[i] == floor_pos) {
                    is_wall = false;
                }
            }

            return is_wall;
        }

        public int get_floors_count() {
            return floors_positions.Count;
        }
    }

    public GameObject floor_prefab;
    public GameObject up_wall_prefab;
    public GameObject down_wall_prefab;
    public GameObject left_wall_prefab;
    public GameObject right_wall_prefab;

    public GameObject left_door_prefab;
    public GameObject right_door_prefab;
    public GameObject up_door_prefab;
    public GameObject down_door_prefab;

    public GameObject player;
    public GameObject keys;
    public GameObject ghost_prefab;
    public GameObject ghost_point;
    public GameObject crawler_prefab;
    public GameObject crawler_point;
    public GameObject firts_aid_prefab;
    public GameObject batterie_prefab;
    public List<GameObject> audio_cubes;
    public GameObject flashlight_prefab;

    public static GameObject ghost;
    public static GameObject crawler;
    public GameObject hide_wall;
    public GameObject can;

    public static List<room> rooms = new List<room>();

    public Vector2Int size; //grid size

    private cell[,] cells; // Pole buňek

    private int current_x = 0;
    private int current_y = 0;

    private item_placement placement;

    public Transform ghost_mesh; 
    public Transform crawler_mesh;

    void Start()
    {
        maze_generator();
        end_door_gen();
        spawn_player();

        keys_spawn();
        batterie_spawn();
        firt_aid_spawn();
        sound_effets_spawn();

        placement =  GetComponent<item_placement>();

        cubes_generator.spawn_room = rooms[Random.Range(0, rooms.Count)];
        spawn_wall();
        for (int i = 0; i < rooms.Count; i++) {
          placement.spawn_object(rooms[i]);
        }
        
        build_nav();
        spawn_ghost();
        spawn_crawler();
    }

    private void build_nav() {
        //https://learn.unity.com/tutorial/runtime-navmesh-generation
        NavMeshSurface mesh_ghost = ghost_mesh.GetComponent<NavMeshSurface>();
        mesh_ghost.BuildNavMesh();
        NavMeshSurface mesh_crawler = crawler_mesh.GetComponent<NavMeshSurface>();
        mesh_crawler.BuildNavMesh();

    }

    void sound_effets_spawn() {
        int num = Random.Range(1, 3);

        for(int i = 0; i < num; i++) {
            room spawn_room = rooms[Random.Range(0, rooms.Count)];
            Vector3 position = spawn_room.get_floor_position();
            GameObject sound_cube = Instantiate(audio_cubes[Random.Range(0, audio_cubes.Count)]);
            sound_cube.transform.position = position + new Vector3(0, -1.236f, 0);
        }
        
    }

    void firt_aid_spawn() {
        int num = Random.Range(1, 20);

        for(int i = 0; i < num; i++) {
            room spawn_room = rooms[Random.Range(0, rooms.Count)];
            Vector3 position = spawn_room.get_floor_position();
            GameObject firt_aid = Instantiate(firts_aid_prefab);
            firt_aid.transform.position = position + new Vector3(0, -1.236f, 0);
        }
    }

    void batterie_spawn() {
        int num = Random.Range(1, 20);

        for(int i = 0; i < num; i++) {
            room spawn_room = rooms[Random.Range(0, rooms.Count)];
            Vector3 position = spawn_room.get_floor_position();
            GameObject batterie = Instantiate(batterie_prefab);
            batterie.transform.position = position + new Vector3(0, -1.1499f, 0);
        }
    }
    void end_door_gen() {
        room end_room = rooms[Random.Range(0, rooms.Count)];
        GameObject door = new GameObject();
        Vector3 destroy_cell = new Vector3();
        string destroy_diretion;
        int cell_num = Random.Range(0, end_room.get_walls_position().Count);


        destroy_cell = end_room.get_walls_position()[cell_num];
        destroy_diretion = end_room.get_walls_direction()[cell_num];
        end_room.destroy_wall(destroy_cell, destroy_diretion);


        if(destroy_diretion == "right") {
            door = Instantiate(right_door_prefab);
            door.transform.position = destroy_cell;
        }
        else if(destroy_diretion == "left") {
            door = Instantiate(left_door_prefab);
            door.transform.position = destroy_cell;

        }
        else if(destroy_diretion == "up") {
            door = Instantiate(up_door_prefab);
            door.transform.position = destroy_cell;

        }
        else if(destroy_diretion == "down") {
            door = Instantiate(down_door_prefab);
            door.transform.position = destroy_cell;

        }

        GameObject door_object = door.transform.Find("wall/DoorV6/01_low").gameObject;
        Destroy(door_object.GetComponent<door_open>());
        door_object.AddComponent<on_pick_full>();
    }



    void spawn_crawler() {
        int num = Random.Range(0, rooms.Count);
        room spawn_room = rooms[num];
        Vector3 position = spawn_room.get_floor_position();
        crawler = Instantiate(crawler_prefab);
        crawler.transform.position = position + new Vector3(0, -1.219f, 0);
        crawler_point.transform.position = position;

        position = rooms[0].get_floor_position();
        Instantiate(can, position + new Vector3(0, -1.2f, 0), Quaternion.identity);
    }

    void spawn_wall() {
        bool valid_pos = true;
        BoxCollider box_collider = hide_wall.GetComponent<BoxCollider>();
        // Získanie rozmier BoxCollider
        Vector3 object_size = box_collider.size;
        Vector3 box_size = object_size * 1f; 
        Vector3 position = rooms[0].get_floor_position();
        valid_pos = rooms[0].check_door(position);
        if(valid_pos) valid_pos = rooms[0].check_wall(position);

        if(!valid_pos) {
            while(!valid_pos) {
                position = rooms[0].get_floor_position();
                valid_pos = rooms[0].check_door(position);
                if(valid_pos) valid_pos = rooms[0].check_wall(position);
            }
        }

        if(!Physics.CheckBox(position, box_size) && valid_pos) {
            Instantiate(hide_wall, position, Quaternion.identity);
        }

       /* int num = Random.Range(0, rooms.Count);

        for(int i = 0; i < num; i++) {
            room spawn_room = rooms[Random.Range(0, rooms.Count)];
            position = spawn_room.get_floor_position();
            

            if(!Physics.CheckBox(position, box_size) && !Physics.CheckBox(position, box_size, Quaternion.identity, item_placement.doors_layer)) {
                Instantiate(hide_wall, position, Quaternion.identity);
            }
           
        }*/
    }

    void spawn_ghost() {
        int num = Random.Range(0, rooms.Count);
        room spawn_room = rooms[num];
        Vector3 position = spawn_room.get_floor_position();
        ghost = Instantiate(ghost_prefab);
        ghost.transform.position = position + new Vector3(0, -1.22f, 0);
        ghost_point.transform.position = position;
        GameObject flashlight_ = Instantiate(flashlight_prefab);
        flashlight_.transform.position = spawn_room.get_floor_position() + new Vector3(0, -1.07f, 0);
    }

    void spawn_player() {
        room spawn_room = rooms[Random.Range(0, rooms.Count)];
        Vector3 position = spawn_room.get_floor_position();
        player.transform.position = position;
    }

    void keys_spawn() {
        room spawn_area;
        for(int i = 0; i < 13; i++) {
            spawn_area = rooms[Random.Range(0, rooms.Count)];
            Vector3 position = spawn_area.get_floor_position();
            GameObject key = Instantiate(keys);
            key.transform.position = position + new Vector3(0, -1.22f, 0);
        }
    }

    void maze_generator()
    {
        cells = new cell[size.x, size.y];
        Stack<Vector4> room_stack = new Stack<Vector4>(); // zásobník s pozíciami izieb

        int width = 0;
        int height = 0;
        int old_width = 0;
        int old_height = 0;
        int old_x = 0;
        int old_y = 0;
        int direction = Random.Range(1, 5); //vpravo, vlavo, hore, dole
        string final_direction = "start";

        Vector3 pos = new Vector3(current_x, 0, current_y);

        //inicializacia cells
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                cells[i, j] = new cell();
            }
        }

        int k = 0;
        bool first_room = true;
        //todo while kym bude v mriezke miesto na umiestnenie izby
        while(k < 50) {
            k++;
            bool generate = true;
           // Debug.Log(room_stack.Count + "roomstack");

            //ak idem do prava hore musim suradnice zmenit skor nez nastavim novu velkost
            //do lava, dole musim najskor zmenit velkost a az potom sa mozem posunut dole/ do lava 
            if(final_direction == "right") {
                current_x += width;
            }
            else if(final_direction == "up") {
                current_y += height;
            }

            width = Random.Range(4, 21); //random range vybera max-1 preto 21 ak chcem 10%
            height = Random.Range(4, 21);

            if(first_room) {
                width = 20;
                height = 20;
                first_room = false;
            }

            if (height % 2 != 0) {
                height += 1;
            }

            if (width % 2 != 0) {
                width += 1;
            }

            //od currentx x sa musim posunut o -width, kontrola ci nie som mimo grid
            if(final_direction == "left") {
                if(current_x - width < 0) {
                    // 0 + width musi byt mensie ako current_x
                    width = change_minus_size(width, current_x, size.x);
                    //ak sa mam kde posunut, posuniem x
                    if(width != 0) {
                        current_x -= width;
                    }
                    else {
                        generate = false;
                    }

                }  
            }
             //od current y sa musim posunut o -height, kontrola ci nie som mimo grid
            else if(final_direction == "down") {
                if(current_y - height < 0) {
                    height = change_minus_size(height, 0 ,current_y);
                    //ak sa mam kde posunut tak sa posuniem
                    if(height != 0) {
                        current_y -= height;
                    }
                    else {
                        generate = false;
                    }
                }
            }
            
            //podmienka ked width a height prekroci hranice gridu
            if(current_x + width > size.x) {
                width = change_plus_size(width, current_x, size.x);
                if(width == 0) {
                    generate = false;
                }
            }

            if(current_y + height > size.y) {
                height = change_plus_size(height, current_y, size.y);
                if(height == 0) {
                    generate = false;
                }
            }

            bool change_width = true;

            //pozeram ci tam kde idem umiestnit uy nieco nie je, ak je skusim zmensit 
            while(true) {
                if(!generate) break;

                bool fits = true;

                //skontrolovat ci sa miestnost zmesti
                for(int i = 0; i < width; i++) {
                    for(int j = 0; j < height; j++) {
                        if(cells[current_x + i, current_y + j].visited == true) {
                            fits = false;
                            break;
                        }
                    }
                    if(!fits) break;
                }

                if(fits) break; //ak sa miestnost zmesti ukonci sa cyklus

                // striedavo meniť šírku alebo výšku
                if (change_width) {
                    width -= 2;
                } else {
                    height -= 2;
                }
                change_width = !change_width; // zmeniť hodnotu pre nasledujúci cyklus

                //dosiahnem najmensiu moznu velkost
                if(width <= 4 || height <= 4) {
                    generate = false;
                    break;
                }

            }

            bool left = true;
            bool right = true;
            bool up = true;
            bool down = true;
            
            //ak sa izba nesmeti do gridu alebo je pozicia generovania obsadena pozriem bunky okolo nej ci je mozne generovat, ak nie vyberiem inu izbu a skusim generovat okolo nej
            if(!generate) {
                if(final_direction == "right") {
                    //ked idem do prava, pravu stranu mam uz prehladanu a staci mi ostatne
                    //musim takto rozdelit lebo inak error ze mimo grid
                    //minimalna vyska a sirka je 4, musim kuknut ci to je volne hore/dole/vlavo od izby

                    //kuknem dole
                    //ak tam nie je dost miesta nema vyznam prehladavat
                    down = check_down_direction(old_x, old_y, old_width);

                    //kuknem hore
                    up = check_up_direction(old_x, old_y, old_height, old_width);

                    //kuknem vlavo
                    left = check_left_direction(old_x, old_y, old_height);

                    //vyhodim zo stacku a idem kukat inde
                    if(!down && !up && !left) {
                        room_stack.Pop();
                        if (room_stack.Count == 0) {
                            break;
                        }
                        Vector4 vector = room_stack.Peek();

                        current_x = (int)vector.x;
                        current_y = (int)vector.y;
                        width = (int)vector.z;
                        height = (int)vector.w;
                        
                    }
                    else {
                        width = old_width;
                        height = old_height;
                        current_x = old_x;
                        current_y = old_y;
                    }

                }

                else if(final_direction == "left") {
                    //up/down -> budu rovnake

                    //kuknem dole
                    down = check_down_direction(old_x, old_y, old_width);

                    //kuknem hore
                    up = check_up_direction(old_x, old_y, old_height, old_width);

                    //kuknem vpravo
                    right = check_right_direction(old_x, old_y, old_width, old_height);

                    if(!up && !down && !right) {
                        room_stack.Pop();
                        if (room_stack.Count == 0) {
                            break;
                        }
                        Vector4 vector = room_stack.Peek();

                        current_x = (int)vector.x;
                        current_y = (int)vector.y;
                        width = (int)vector.z;
                        height = (int)vector.w;
                    }
                    else {
                        width = old_width;
                        height = old_height;
                        current_x = old_x;
                        current_y = old_y;
                    }

                }

                else if(final_direction == "up") {
                    right = check_right_direction(old_x, old_y, old_width, old_height);
                    down = check_down_direction(old_x, old_y, old_width);
                    left = check_left_direction(old_x, old_y, old_height);

                    if(!left && !down && !right) {
                        room_stack.Pop();
                        if (room_stack.Count == 0) {
                            break;
                        }
                        Vector4 vector = room_stack.Peek();

                        current_x = (int)vector.x;
                        current_y = (int)vector.y;
                        width = (int)vector.z;
                        height = (int)vector.w;
                    }
                    else {
                        width = old_width;
                        height = old_height;
                        current_x = old_x;
                        current_y = old_y;
                    }

                }

                else if(final_direction == "down") {
                    up = check_up_direction(old_x, old_y, old_height, old_width);
                    left = check_left_direction(old_x, old_y, old_height);
                    right = check_right_direction(old_x, old_y, old_width, old_height);

                    if(!up && !left && !right) {
                        room_stack.Pop();
                        if (room_stack.Count == 0) {
                            break;
                        }
                        Vector4 vector = room_stack.Peek();

                        current_x = (int)vector.x;
                        current_y = (int)vector.y;
                        width = (int)vector.z;
                        height = (int)vector.w;
                    }
                    else {
                        width = old_width;
                        height = old_height;
                        current_x = old_x;
                        current_y = old_y;
                    }
                }
            }

            if(generate) {

                //ked je vsetko okej nastavim poziciu a vygenerujem izbu 
                pos = new Vector3(current_x, 0, current_y);
                //Debug.Log(width + "suradnice su" + height);

                generate_room(width, height, pos); //vygenerujem izbu
                //ulozim izbu do stacku
                room_stack.Push(new Vector4(current_x, current_y, width, height));
                //oznacenie buniek za obsadene
   
                for(int i = 0; i < width; i++)
                {
                    for(int j = 0; j < height; j++)
                    {
                        cells[current_x + i, current_y + j].visited = true;
                        cells[current_x + i, current_y + j].room_on_cell = rooms[rooms.Count-1];
                    }
                }
                
                GameObject door;
                List<Vector3> tmp_cell = new List<Vector3>();
                Vector3 destroy_cell = new Vector3();

                if(final_direction == "right") {
                    //prejdem celu vysku a zistim ktore bunky su susedne
                    for(int i = 0; i < height; i++) {
                        //tie ktore su spolocne pridam do listu
                        if(cells[current_x - 2, current_y + i].visited) {
                            tmp_cell.Add(new Vector3(current_x, 0, current_y + i));
                        }
                    }
                    //destroy_cell = tmp_cell[Random.Range(0, tmp_cell.Count - 2)];
                    //Debug.Log(tmp_cell.Count );
                     destroy_cell = tmp_cell[Random.Range(0, tmp_cell.Count - 1)];

                    if (destroy_cell.x % 2 != 0)
                    {
                        destroy_cell.x += 1;
                    }

                    if (destroy_cell.z % 2 != 0)
                    {
                        destroy_cell.z += 1;
                    }

                    rooms[rooms.Count-1].destroy_wall(destroy_cell, "left");
                    cells[current_x - 2, current_y].room_on_cell.destroy_wall(new Vector3(destroy_cell.x - 2, 0, destroy_cell.z), "right");
                    door = Instantiate(left_door_prefab);
                    door.transform.position = destroy_cell;

                }
                else if(final_direction == "up") {
                    for(int i = 0; i < width; i++) {
                        //tie ktore su spolocne pridam do listu
                        if(cells[current_x + i, current_y - 2].visited) {
                            tmp_cell.Add(new Vector3(current_x + i, 0, current_y));
                        }
                    }
                    //destroy_cell = tmp_cell[Random.Range(0, tmp_cell.Count - 2)];
                     //Debug.Log(tmp_cell.Count );
                    destroy_cell = tmp_cell[Random.Range(0, tmp_cell.Count - 1)];

                    if (destroy_cell.x % 2 != 0)
                    {
                        destroy_cell.x += 1;
                    }

                    if (destroy_cell.z % 2 != 0)
                    {
                        destroy_cell.z += 1;
                    }

                    rooms[rooms.Count-1].destroy_wall(destroy_cell, "down");
                    cells[current_x, current_y - 2].room_on_cell.destroy_wall(new Vector3(destroy_cell.x, 0, destroy_cell.z - 2), "up");
                    door = Instantiate(down_door_prefab);
                    door.transform.position = destroy_cell;

                }
                else if(final_direction == "left") {
                    Vector3 tmp_pos = pos;
                    tmp_pos.x += width;
                   // Debug.Log(tmp_pos.ToString());

                    for(int i = 0; i < height; i++) {
                        //tie ktore su spolocne pridam do listu
                        if(cells[current_x + width, current_y + i].visited) {
                            tmp_cell.Add(new Vector3(current_x + width - 2, 0, current_y + i));
                        }
                    }
                    //destroy_cell = tmp_cell[Random.Range(0, tmp_cell.Count - 2)];
                     //Debug.Log(tmp_cell.Count );
                    destroy_cell = tmp_cell[Random.Range(0, tmp_cell.Count - 1)];

                    if (destroy_cell.x % 2 != 0)
                    {
                        destroy_cell.x += 1;
                    }

                    if (destroy_cell.z % 2 != 0)
                    {
                        destroy_cell.z += 1;
                    }

                    rooms[rooms.Count-1].destroy_wall(destroy_cell, "right");
                    cells[current_x + width + 2, current_y].room_on_cell.destroy_wall(new Vector3(destroy_cell.x + 2, 0, destroy_cell.z), "left");
                    door = Instantiate(right_door_prefab);
                    door.transform.position = destroy_cell;

                }
                else if(final_direction == "down") {
                    for(int i = 0; i < width; i++) {
                        //tie ktore su spolocne pridam do listu
                        if(cells[current_x + i, current_y + height /*+ 2*/].visited) {
                            tmp_cell.Add(new Vector3(current_x + i, 0, current_y + height - 2));
                        }
                    }
                    //destroy_cell = tmp_cell[Random.Range(0, tmp_cell.Count - 2)];
                    // Debug.Log(tmp_cell.Count );
                    destroy_cell = tmp_cell[Random.Range(0, tmp_cell.Count - 1)];

                    if (destroy_cell.x % 2 != 0)
                    {
                        destroy_cell.x += 1;
                    }

                    if (destroy_cell.z % 2 != 0)
                    {
                        destroy_cell.z += 1;
                    }

                    rooms[rooms.Count-1].destroy_wall(destroy_cell, "up");
                    cells[current_x, current_y + height + 2].room_on_cell.destroy_wall(new Vector3(destroy_cell.x, 0, destroy_cell.z + /*height +*/ 2), "down");
                    door = Instantiate(up_door_prefab);
                    door.transform.position = destroy_cell;

                }
            }
            
            //random sa posuniem, dolava do dola podla novej vysky a sirky
            //toto zmenit pravdepoodbne podla aktualej situacie v mriezke
            switch(direction) {
                case 1: 
                      final_direction = "right";
                      break;  
                case 2:
                        final_direction = "up";
                        break;
                case 3:
                        final_direction = "left"; 
                        break;
                case 4:
                        final_direction = "down";
                        break;
            }

            direction = Random.Range(1,5);
            old_width = width;
            old_height = height;
            old_x = current_x;
            old_y = current_y;
        }
    }

    bool check_right_direction(int x, int y, int width, int height) {
        bool ret_val = true;
        //ak x + width + min sirka je vacsia ako grid, nema vyznam pokracovat
        if(x + width + 4 > size.x) {
            ret_val = false;
        }
        else {
            //x sa zvacsuje, y sa zvacsuje
            //predpoklada sa ze izba je 4x4, vyskusat ci 4x4 nie su visited
            //najskor skontrolujem ci od hora mozem pozerat, si nepresiahnem grid
            if(y + height + 4 > size.y) {
                ret_val = false;
            }
            else {
                for(int i = 1; i < 5; i++) {
                    for(int j = 0; j < 3; j++) {
                        if(cells[x + width + i, y + j].visited) {
                            ret_val = false;
                            break;
                        }
                    }
                    if(!ret_val) break;
                }
            }
            
        }

        return ret_val;
    }

    bool check_left_direction(int x, int y, int height) {
        bool ret_val = true;
        if(x - 4 < 0) {
            ret_val = false;
        }
        else {
            //x sa zmensuje y zvacsuje
            if(y + height + 4 > size.y) {
                ret_val = false;
            }
            else {
                for(int i = 1; i < 5; i++) {
                    for(int j = 0; j < 3; j++) {
                        if(cells[x - i, y + j].visited) {
                            ret_val = false;
                            break;
                        }
                    }
                    if(!ret_val) break;
                }
            }
            
        }

        return ret_val;
    }

    bool check_up_direction(int x, int y, int height, int width) {
        bool ret_val = true;
        if(y + height + 4 > size.x) {
            ret_val = false;
        }
        else {
            if(x + width + 4 > size.x) {
                ret_val = false;
            }
            else {
               for(int i = 1; i < 5; i++) {
                    for(int j = 0; j < 3; j++) {
                        if(cells[x + j, y + height + i].visited) {
                            ret_val = false;
                            break;
                        }
                    }
                    if(!ret_val) break;
                } 
            }
            
        }

        return ret_val;
    }

    bool check_down_direction(int x, int y, int width) {
        bool ret_val = true;
        if(y - 4 < 0) {
            ret_val = false;
        }
        else {
        //ak ma, kuknem ci tam vobec je este miesto pre vygenerovanie novej izby
            if(x + width + 4 > size.x) {
                ret_val = false;
            }
            else {
               for(int i = 1; i < 5; i++) {
                    for(int j = 0; j < 3; j++) {
                        if(cells[x + j, y - i].visited) {
                            ret_val = false;
                            break;
                        }
                    }
                    if(!ret_val) break;
                } 
            }
            
        }

        return ret_val;
    }

    //upravujem a skusam velkost vramci gridu ci sa zmesti, ci nie su bunky obsadene riesim neskor
    //ak sa nezmesti skusim postupne zmensovat velkost
    int change_plus_size(int room_size, int current_pos, int grid_size) {
        int new_size = room_size - 2;

        while(true) {
            if(new_size + current_pos > grid_size) {
                new_size -= 2;
            }
            else if(new_size == 2) {
                new_size = 0;
                break;
            }
            else {
                break;
            }
        }

        return new_size;
    } 

    int change_minus_size(int room_size, int current_pos, int grid_size) {
        int new_size = room_size - 2;

        while(true) {
            if(current_pos - new_size < 0) {
                new_size -= 2;
            }
            else if(new_size == 2) {
                new_size = 0;
                break;
            }
            else {
                break;
            }
        }

        return new_size;
    } 

    void generate_room(int width, int height, Vector3 position) {
        GameObject room = new GameObject();
        room.transform.position = position;
        List<Vector3> tmp_walls_positions = new List<Vector3>();
        List<string> tmp_walls_directions = new List<string>();
        List<GameObject> walls = new List<GameObject>();
        List<Vector3> tmp_floors_positions = new List<Vector3>();

       // BoxCollider boxCollider = room.AddComponent<BoxCollider>();
        room.gameObject.tag="Room";


        for(int i = 0; i < width; i += 2) {
            for(int j = 0; j < height; j += 2) {
                GameObject tile = Instantiate(floor_prefab, room.transform);
                tile.transform.position = new Vector3(i, 0, j) + position;
                tmp_floors_positions.Add(new Vector3(i, 0, j) + position);

                if (i == 0) {
                    GameObject wall = Instantiate(left_wall_prefab, room.transform);
                    wall.transform.position = new Vector3(i, 0, j) + position;
                    tmp_walls_positions.Add(new Vector3(i, 0, j) + position);
                    tmp_walls_directions.Add("left");
                    walls.Add(wall);
                } else if (i == width - 2) {
                    GameObject wall = Instantiate(right_wall_prefab, room.transform);
                    wall.transform.position = new Vector3(i, 0, j) + position;
                    tmp_walls_positions.Add(new Vector3(i, 0, j) + position);
                    tmp_walls_directions.Add("right");
                    walls.Add(wall);
                }

                if (j == 0) {
                    GameObject wall = Instantiate(down_wall_prefab, room.transform);
                    wall.transform.position = new Vector3(i, 0, j) + position;
                    tmp_walls_positions.Add(new Vector3(i, 0, j) + position);
                    tmp_walls_directions.Add("down");
                    walls.Add(wall);
                } else if (j == height - 2) {
                    GameObject wall = Instantiate(up_wall_prefab, room.transform);
                    wall.transform.position = new Vector3(i, 0, j) + position;
                    tmp_walls_positions.Add(new Vector3(i, 0, j) + position);
                    tmp_walls_directions.Add("up");
                    walls.Add(wall);
                }
            }
        }


        rooms.Add(new room(width, height, position, new Vector2Int(width, height), room, tmp_walls_positions, tmp_walls_directions, walls, tmp_floors_positions));
    }
}
