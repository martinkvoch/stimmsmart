﻿using System;
using System.Windows.Forms;
using MDM.Data;
using MDM.Windows;

namespace MDM.Controls
{
    public enum PanelButton : byte { Navigator = 1, Insert = 2, Delete = 4, Edit = 8, Wipe = 16, Undelete = 32 }
    public enum PanelLayout { ReadOnly, DeleteOnly, WODelete, Edit, Undelete }

    /// <summary>
    /// Bázová třída pro panely umístěné na hlavním oknu.
    /// Je určena k rozlišení panelů při přepínání a řeší vzhled panelů
    /// </summary>
    public class MDMPanel : UserControl
    {
        public MDMTable DBObject = null;
        public ToolStripMenuItem PanelMenu;
        private PanelLayout panelLayout = PanelLayout.ReadOnly;
        private string cmd;

        protected string Cmd
        {
            get { return cmd; }
            private set { cmd = value; }
        }

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
            this.DBObject = dbObject;
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
