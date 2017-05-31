﻿using System;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;

using MDM.Data;

namespace MDM.Controls
{
    //TODO: do navigace dataGridu přidat pageUp a pageDown
    /// <summary>
    /// Databázový panel. Zobrazuje data daná SQL příkazem SELECT a případně s nimi manipuluje.
    /// Jednotlivé komponenty panelu jsou konfigurovatelné.
    /// </summary>
    public partial class DBPanel : MDMPanel
    {
        #region Privátní funkce
        private void setLayout()
        {
            int layout = PanelButtons;

            nbMoveFirst.Visible = nbMovePrevious.Visible = nbMoveNext.Visible = nbMoveLast.Visible = nbPosition.Visible = nbCount.Visible = ((layout & (int)PanelButton.Navigator) != 0);
            nbAddNew.Visible = ((layout & (int)PanelButton.Insert) != 0);
            nbDelete.Visible = ((layout & (int)PanelButton.Delete) != 0);
            nbEdit.Visible = ((layout & (int)PanelButton.Edit) != 0);
            nbWipe.Visible = ((layout & (int)PanelButton.Undelete) != 0);
            nbUndelete.Visible = ((layout & (int)PanelButton.Undelete) != 0);
        }

        private void setButtons()
        {
            bool enabled = (dataGrid.Rows.Count > 0) && !object.ReferenceEquals(dataGrid.Rows[0].Cells[0].Value, null);

            if((PanelButtons & (int)PanelButton.Delete) > 0) nbDelete.Enabled = enabled;
            if((PanelButtons & (int)PanelButton.Edit) > 0) nbEdit.Enabled = enabled;
            if((PanelButtons & (int)PanelButton.Wipe) > 0) nbWipe.Enabled = enabled;
            if((PanelButtons & (int)PanelButton.Undelete) > 0) nbUndelete.Enabled = enabled;
        }
        #endregion

        public DBPanel()
        {
            InitializeComponent();
        }

        #region Veřejné funkce
        public override void Fill(string cmd = null)
        {
            if(DBObject != null)
            {
                cmd = cmd ?? DBObject.SelectCmd();
                using(SQLiteConnection conn = Database.CreateConnection())
                {
                    int pos = bindingSource.Position;

                    dbpDataSet.Clear();
                    using(SQLiteDataAdapter da = new SQLiteDataAdapter(cmd, conn)) da.Fill(dbpDataSet);
                    bindingSource.Dispose();
                    bindingSource = new BindingSource();
                    bindingSource.DataSource = dbpDataSet.Tables[0].DefaultView;
                    dataGrid.DataSource = bindingSource;
                    dbpNavigator.BindingSource = bindingSource;
                    bindingSource.Position = pos;
                }
                setButtons();
            }
        }

        /// <summary>
        /// Otevře databázový panel s daným příkazem select, který poskytne data.
        /// </summary>
        /// <param name="dbt">databázový objekt. Třída přes kterou se získají data a operace.</param>
        /// <param name="panMenu">pokud není null, na hlavním pruhu nabídek se zobrazí lokální nabídka pro tento panel.</param>
        /// <param name="layout">určuje, jaké funkce nad daty se mohou provádět a jak budou data zobrazována.</param>
        /// <param name="cmd">určuje příkaz SQL select, jehož výstupem bude panel naplněn. Pokud není zadán, použije se standardní SelectCmd().</param>
        public override void Open(MDMTable dbt, ToolStripMenuItem panMenu = null, PanelLayout layout = PanelLayout.ReadOnly, string cmd = null)
        {
            base.Open(dbt, panMenu, layout, cmd);
            setLayout();
            Fill(Cmd);
        }

        /// <summary>
        /// Uzavře panel a předá zaměření na hlavní panel.
        /// </summary>
        public override void Close()
        {
            dbpDataSet.Clear();
            bindingSource.Dispose();
            bindingSource = new BindingSource();
            base.Close();
        }

        public object CurrentID()
        {
            return dataGrid.SelectedRows != null && dataGrid.SelectedRows.Count > 0 ? dataGrid.SelectedRows[0].Cells[0].Value : null;
        }
        #endregion

        #region Zavření panelu
        private void nbClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Pohyb po tabulce
        private void nbMoveFirst_Click(object sender, EventArgs e)
        {
            bindingSource.MoveFirst();
        }

        private void nbMovePrevious_Click(object sender, EventArgs e)
        {
            bindingSource.MovePrevious();
        }

        private void nbMoveNext_Click(object sender, EventArgs e)
        {
            bindingSource.MoveNext();
        }

        private void nbMoveLast_Click(object sender, EventArgs e)
        {
            bindingSource.MoveLast();
        }
        #endregion

        #region Editace tabulky
        private void nbAddNew_Click(object sender, EventArgs e)
        {
            if(DBObject != null)
            {
                DBObject.Add();
                Fill(Cmd);
            }
        }

        private void nbEdit_Click(object sender, EventArgs e)
        {
            if(DBObject != null)
            {
                object id = CurrentID();

                if(id != null)
                {
                    DBObject.Edit(id);
                    Fill(Cmd);
                }
            }
        }

        private void nbDelete_Click(object sender, EventArgs e)
        {
            if(DBObject != null)
            {
                object id = CurrentID();

                if(id != null)
                {
                    DBObject.Delete(id);
                    Fill(Cmd);
                }
            }
        }

        public void DeleteSelection()
        {
            if(DBObject != null)
            {
                DBObject.Delete(dataGrid.SelectedRows.OfType<DataGridViewRow>().Select(r => r.Cells[0].Value).ToArray());
                Fill(Cmd);
            }
        }

        private void nbWipe_Click(object sender, EventArgs e)
        {
            if(DBObject != null)
            {
                object id = CurrentID();

                if(id != null)
                {
                    DBObject.Wipe(id);
                    Fill(Cmd);
                }
            }
        }

        private void nbUndelete_Click(object sender, EventArgs e)
        {
            if(DBObject != null)
            {
                object id = CurrentID();

                if(id != null)
                {
                    DBObject.Undelete(id);
                    Fill(Cmd);
                }
            }
        }
        #endregion
    }
}
