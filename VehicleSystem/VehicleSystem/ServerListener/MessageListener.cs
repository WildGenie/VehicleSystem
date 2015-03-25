using System;

namespace VehicleSystem.ServerListener
{
    class MessageListener : IListener
    {
        private Form1 main;

        public MessageListener(Form1 main)
        {
            this.main = main;
        }

        public void messageRecieved(String fromRegId, String command)
        {
            Console.WriteLine("Command recieved: " + command);
            switch (command)
            {
                case Globals.message_request_car_info :
                {
                    main._server.sendSingleCarInfoToAll("test","test","test","test");
                    break;
                }
            }
        }
    }
}
