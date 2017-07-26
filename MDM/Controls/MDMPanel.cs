using System;
using System.Windows.Forms;
using MDM.Data;
using MDM.Windows;

namespace MDM.Controls
{
    public enum PanelButton : byte { Navigator = 0x01, Insert = 0x02, Delete = 0x04, Edit = 0x08, Wipe = 0x10, Undelete = 0x20, Filter = 0x40 }
    public enum PanelLayout { ReadOnly, DeleteOnly, WODelete, Edit, Undelete, WFilter }

    /// <summary>
    /// Bázová třída pro panely umístěné na hlavním oknu.
    /// Je určena k rozlišení panelů při přepínání a řeší vzhled panelů
    /// </summary>
    public class MDMPanel : UserControl
    {
        public MDMTable DBObject = null;
        public ToolStripMenuItem PanelMenu;
        private PanelLayout panelLayout = PanelLayout.ReadOnly;
        //private string cmd;

        protected string Cmd { get; set; }
        //{
        //    get { return cmd; }
        //    private set { cmd = value; }
        //}

        protected void switchToPanel(MDMPanel pan = null)
        {
            (Parent as wMain).SwitchToPanel(pan);
            if(PanelMenu != null) PanelMenu.Visible = true;
        }

        /// <summary>
        /// Konfigurace tlačítek na panelu
        /// </summary>
        protected byte PanelButtons 
        { 
            get
            {
                byte res = (byte)PanelButton.Navigator;

                switch(panelLayout)
                {
                    case PanelLayout.DeleteOnly:
                        res += (byte)PanelButton.Delete;
                        break;
                    case PanelLayout.WODelete:
                        res += (byte)PanelButton.Insert;
                        res += (byte)PanelButton.Edit;
                        break;
                    case PanelLayout.Edit:
                        res += (byte)PanelButton.Insert;
                        res += (byte)PanelButton.Delete;
                        res += (byte)PanelButton.Edit;
                        break;
                    case PanelLayout.Undelete:
                        res += (byte)PanelButton.Insert;
                        res += (byte)PanelButton.Delete;
                        res += (byte)PanelButton.Edit;
                        res += (byte)PanelButton.Undelete;
                        res += (byte)PanelButton.Wipe;
                        break;
                    case PanelLayout.WFilter:
                        res += (byte)PanelButton.Filter;
                        break;
                }
                return res;
            }
        }

        public virtual void Fill(string cmd = null) { }

        public virtual void Open(ToolStripMenuItem panMenu = null) 
        {
            PanelMenu = panMenu;
            Fill();
            switchToPanel(this);
        }

        public virtual void Open(MDMTable dbObject, ToolStripMenuItem panMenu = null, PanelLayout layout = PanelLayout.ReadOnly, string cmd = null) 
        {
            DBObject = dbObject;
            PanelMenu = panMenu;
            panelLayout = layout;
            Cmd = cmd;
            Fill(cmd);
            switchToPanel(this);
        }

        public virtual void Close() 
        {
            if(PanelMenu != null)
            {
                PanelMenu.Visible = false;
                PanelMenu = null;
            }
            switchToPanel();
        }
    }
}
