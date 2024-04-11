using Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GARVIS.Servises
{
    public class cshack
    {
        Mem mem = new Mem();
        string x = "hw.dll+007CD154,3AC";
        string y = "hw.dll+007CD154,3B4";
        string z = "hw.dll+007CD154,3B0";
        string health = "hw.dll+007CD154,504";
        string money = "hw.dll+001AFF74,E54";



        public void TeleportToTeror() => TeleportToTerror();
        public void TeleportToMeant() => TeleportToMent();
        public void Health() => FullHealth();
        public void GetMoney() => Money();

        private void TeleportToTerror()
        {
            if (OpenProcess())
            {
                mem.WriteMemory(x, "long", "3293689930");
                mem.WriteMemory(z, "long", "1159132380");
                mem.WriteMemory(y, "long", "1136264192");
            }
          
        }
        private void FullHealth()
        {
            if (OpenProcess())
            {
                mem.WriteMemory(health, "float", "1000");
            }
        }
        private void TeleportToMent()
        {
            if (OpenProcess())
            {
                mem.WriteMemory(x, "long", "1142947840");
                mem.WriteMemory(z, "long", "1127562124");
                mem.WriteMemory(y, "long", "1108353024");
            }
        } 
        private void Money()
        {
            if (OpenProcess())
            {
                
                mem.WriteMemory(money, "float", "16000");
            }
        }
        private bool OpenProcess()
        {
            int ProcId = mem.GetProcIdFromName("hl");
            if (ProcId > 0)
            {
                mem.OpenProcess(ProcId);
                return true;
            }
            else
            {
                MessageBox.Show("Error game is closed");
                return false;
            }
        }

        
    }
}
