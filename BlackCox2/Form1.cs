using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JRPC_Client;
using MetroFramework;
using MetroFramework.Forms;
using XDevkit;

namespace BlackCox2
{
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        IXboxConsole Console;
        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (Console.Connect(out Console))
            {
                metroLabel1.Text = "Connected to Console!";
            }
            else
            {
                metroLabel1.Text = "Error: Could not connect.";
            }
            JRPC.XNotify(Console, "Force Ops II Connected! Enjoy :) -Klepto");
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public uint BO2CMD = 2185237984;
        public uint BO2_StatEntry = 2215380224;

        private void metroToggle1_CheckedChanged(object sender, EventArgs e)
        {
            if (metroToggle1.Checked == false) // If off turn on
            {
                if (!JRPC.Connect(this.Console, out this.Console, "default"))
                {
                    metroLabel1.Text = "Connect console first!";
                    return;
                }
                JRPC.CallVoid(this.Console, this.BO2CMD, new object[2]
                {
                    (object) 0,
                    (object) "party_connectToOthers 01; partyMigrate_disabled 00"
                });
            }
            else if (metroToggle1.Checked == true) // If on turn off
            {
                if (!JRPC.Connect(this.Console, out this.Console, "default"))
                {
                    metroLabel1.Text = "Connect console first!";
                    return;
                }
                JRPC.CallVoid(this.Console, this.BO2CMD, new object[2]
                {
                    (object) 0,
                    (object) "party_connectToOthers 00; partyMigrate_disabled 01; sv_endGameIfISuck 0; badhost_endgameifisuck 0\x200B; set party_gamestarttimelength 2; set party_pregamestarttimerlength 2; set party_timer 25"
                });
            }
        }

        public uint GET_ALIGNED_DWORD(uint x)
        {
            return (uint)((int)x + 3 & -4);
        }


        private void metroButton3_Click(object sender, EventArgs e)
        {
            string startupPath = Application.StartupPath;
            uint num1 = 1076887552;
            uint num2;
            if (!JRPC.Connect(this.Console, out this.Console, "default"))
            {
                metroLabel1.Text = "Connect console first!";
                return;
            }
            else try
            {
                uint alignedDword1 = this.GET_ALIGNED_DWORD(num1 + 1U);
                byte[] numArray1 = File.ReadAllBytes(startupPath + "\\GSC\\Jiggy4.5\\maps\\mp\\_ambientpackage.gsc");
                JRPC.SetMemory(this.Console, 2199829440U, ((IEnumerable<byte>)BitConverter.GetBytes(alignedDword1)).Reverse<byte>().ToArray<byte>());
                JRPC.SetMemory(this.Console, alignedDword1, new byte[numArray1.Length + 1]);
                JRPC.SetMemory(this.Console, alignedDword1, numArray1);
                uint alignedDword2 = this.GET_ALIGNED_DWORD(alignedDword1 + (uint)(numArray1.Length + 1) + 1U);
                byte[] numArray2 = File.ReadAllBytes(startupPath + "\\GSC\\Jiggy4.5\\maps\\mp\\_development_dvars.gsc");
                JRPC.SetMemory(this.Console, 2199829632U, ((IEnumerable<byte>)BitConverter.GetBytes(alignedDword2)).Reverse<byte>().ToArray<byte>());
                JRPC.SetMemory(this.Console, alignedDword2, new byte[numArray2.Length + 1]);
                JRPC.SetMemory(this.Console, alignedDword2, numArray2);
                uint alignedDword3 = this.GET_ALIGNED_DWORD(alignedDword2 + (uint)(numArray2.Length + 1) + 1U);
                byte[] numArray3 = File.ReadAllBytes(startupPath + "\\GSC\\Jiggy4.5\\maps\\mp\\_sticky_grenade.gsc");
                JRPC.SetMemory(this.Console, 2199829980U, ((IEnumerable<byte>)BitConverter.GetBytes(alignedDword3)).Reverse<byte>().ToArray<byte>());
                JRPC.SetMemory(this.Console, alignedDword3, new byte[numArray3.Length + 1]);
                JRPC.SetMemory(this.Console, alignedDword3, numArray3);
                uint alignedDword4 = this.GET_ALIGNED_DWORD(alignedDword3 + (uint)(numArray3.Length + 1) + 1U);
                byte[] numArray4 = File.ReadAllBytes(startupPath + "\\GSC\\Jiggy4.5\\maps\\mp\\gametypes\\_rank.gsc");
                JRPC.SetMemory(this.Console, 2199830484U, ((IEnumerable<byte>)BitConverter.GetBytes(alignedDword4)).Reverse<byte>().ToArray<byte>());
                JRPC.SetMemory(this.Console, alignedDword4, new byte[numArray4.Length + 1]);
                JRPC.SetMemory(this.Console, alignedDword4, numArray4);
                num2 = alignedDword4 + (uint)(numArray4.Length + 1);
                    metroLabel1.Text = "Jiggy Menu Injected!";
            }
            catch (Exception ex)
            {

                MessageBox.Show("Failed To Inject Jiggy V4.5");
            }
        }
    }
}
