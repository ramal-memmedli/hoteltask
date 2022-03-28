using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelTask.Models
{
    public class Room
    {
        private static int _roomId;

        private string _roomName;
        private int _price;
        private int _personCapacity;

        private static Room[] _allRooms = new Room[0];

        public int RoomId { get; private set; }
        public string RoomName { get { return _roomName; } set { if(!String.IsNullOrEmpty(value)) _roomName = value; } }
        public int Price { get { return _price; } set { if (value > 0) _price = value; } }
        public int PersonCapacity { get { return _personCapacity; } set { if (value > 0 && value < 10) _personCapacity = value; } }
        public bool IsAvailable { get; set; }

        static Room()
        {
            _roomId = 0;
        }

        private Room()
        {
            RoomId = ++_roomId;
            Array.Resize(ref _allRooms, _allRooms.Length + 1);
            _allRooms[^1] = this;
        }

        public Room(string roomName, int price, int personCapacity, bool isAvailable = true) : this()
        {
            RoomName = roomName;
            Price = price;
            PersonCapacity = personCapacity;
            IsAvailable = isAvailable;
        }

        public static void CreateRoomByConsole()
        {

            Console.WriteLine("-------------------------------------------\n" +
                             $"------------- Create new room -------------\n" +
                              "-------------------------------------------");
        TryAgain:
            string newRoomName = Program.GetStringInputByConsole("room name");
            int? newRoomPrice = Program.GetIntInputByConsole("room price");
            if (newRoomPrice <= 0 || newRoomPrice == null)
            {
                Console.Clear();
                Console.WriteLine("-------------------------------------------\n" +
                                     $"---- Price can't be zero or under zero! ---\n" +
                                      "-------------------------------------------");
                goto TryAgain;
            }

            int? newPersonCapacity = Program.GetIntInputByConsole("person capacity");
            if (newPersonCapacity <= 0 || newPersonCapacity > 10)
            {
                Console.Clear();
                Console.WriteLine("-------------------------------------------\n" +
                                 $"Person capacity can only be between 1 - 10 \n" +
                                  "-------------------------------------------");
                goto TryAgain;
            }

            _ = new Room(newRoomName,(int) newRoomPrice,(int) newPersonCapacity);
            Console.Clear();
            Console.WriteLine("-------------------------------------------\n" +
                             $"-------- Room created successfully --------\n" +
                              "-------------------------------------------");
        }

        public static void PrintAllRooms()
        {
            if(_allRooms.Length == 0)
            {
                Console.WriteLine("-------------------------------------------\n" +
                                 $"------------- There is no room ------------\n" +
                                  "-------------------------------------------");
            }
            else
            {
                Console.WriteLine("-------------------------------------------\n" +
                             $"---------------- All rooms ----------------\n" +
                              "-------------------------------------------");
                foreach (Room room in _allRooms)
                {
                    room.ShowInfo();
                }
            }
        }

        public void ShowInfo()
        {
            Console.WriteLine("-------------------------------------------\n" +
                             $"---------------- Room info ----------------\n" +
                              "-------------------------------------------");
            Console.WriteLine(ToString());
        }

        public static bool CheckId(int id)
        {
            return Array.Exists(_allRooms, room => room.RoomId == id);
        }

        public static Room GetRoom(int id)
        {
            return _ = Array.Find(_allRooms, room => room.RoomId == id);
        }

        public override string ToString()
        {
            return $"Id - {RoomId} | Room name - {RoomName} | Room price - {Price} | Person capacity of room - {PersonCapacity} | Is room available for reservation? - {IsAvailable}";
        }
    }
}
