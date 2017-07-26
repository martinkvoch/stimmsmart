namespace MDM.Controls
{
    partial class DBPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBPanel));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dbpNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.nbAddNew = new System.Windows.Forms.ToolStripButton();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nbCount = new System.Windows.Forms.ToolStripLabel();
            this.nbMoveFirst = new System.Windows.Forms.ToolStripButton();
            this.nbMovePrevious = new System.Windows.Forms.ToolStripButton();
            this.nbPosition = new System.Windows.Forms.ToolStripTextBox();
            this.nbMoveNext = new System.Windows.Forms.ToolStripButton();
            this.nbMoveLast = new System.Windows.Forms.ToolStripButton();
            this.nbDelete = new System.Windows.Forms.ToolStripButton();
            this.nbClose = new System.Windows.Forms.ToolStripButton();
            this.nbEdit = new System.Windows.Forms.ToolStripButton();
            this.nbWipe = new System.Windows.Forms.ToolStripButton();
            this.nbUndelete = new System.Windows.Forms.ToolStripButton();
            this.nbFilter = new System.Windows.Forms.ToolStripDropDownButton();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.dbpDataSet = new MDM.Data.DBDataSet();
            ((System.ComponentModel.ISupportInitialize)(this.dbpNavigator)).BeginInit();
            this.dbpNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbpDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dbpNavigator
            // 
            this.dbpNavigator.AddNewItem = this.nbAddNew;
            this.dbpNavigator.BindingSource = this.bindingSource;
            this.dbpNavigator.CountItem = this.nbCount;
            this.dbpNavigator.DeleteItem = null;
            this.dbpNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.dbpNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nbMoveFirst,
            this.nbMovePrevious,
            this.nbPosition,
            this.nbCount,
            this.nbMoveNext,
            this.nbMoveLast,
            this.nbAddNew,
            this.nbDelete,
            this.nbClose,
            this.nbEdit,
            this.nbWipe,
            this.nbUndelete,
            this.nbFilter});
            resources.ApplyResources(this.dbpNavigator, "dbpNavigator");
            this.dbpNavigator.MoveFirstItem = this.nbMoveFirst;
            this.dbpNavigator.MoveLastItem = this.nbMoveLast;
            this.dbpNavigator.MoveNextItem = this.nbMoveNext;
            this.dbpNavigator.MovePreviousItem = this.nbMoveNext;
            this.dbpNavigator.Name = "dbpNavigator";
            this.dbpNavigator.PositionItem = this.nbPosition;
            // 
            // nbAddNew
            // 
            this.nbAddNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.nbAddNew, "nbAddNew");
            this.nbAddNew.Name = "nbAddNew";
            this.nbAddNew.Click += new System.EventHandler(this.nbAddNew_Click);
            // 
            // nbCount
            // 
            this.nbCount.Name = "nbCount";
            resources.ApplyResources(this.nbCount, "nbCount");
            // 
            // nbMoveFirst
            // 
            this.nbMoveFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.nbMoveFirst, "nbMoveFirst");
            this.nbMoveFirst.Name = "nbMoveFirst";
            this.nbMoveFirst.Click += new System.EventHandler(this.nbMoveFirst_Click);
            // 
            // nbMovePrevious
            // 
            this.nbMovePrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.nbMovePrevious, "nbMovePrevious");
            this.nbMovePrevious.Name = "nbMovePrevious";
            this.nbMovePrevious.Click += new System.EventHandler(this.nbMovePrevious_Click);
            // 
            // nbPosition
            // 
            resources.ApplyResources(this.nbPosition, "nbPosition");
            this.nbPosition.Name = "nbPosition";
            // 
            // nbMoveNext
            // 
            this.nbMoveNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.nbMoveNext, "nbMoveNext");
            this.nbMoveNext.Name = "nbMoveNext";
            this.nbMoveNext.Click += new System.EventHandler(this.nbMoveNext_Click);
            // 
            // nbMoveLast
            // 
            this.nbMoveLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.nbMoveLast, "nbMoveLast");
            this.nbMoveLast.Name = "nbMoveLast";
            this.nbMoveLast.Click += new System.EventHandler(this.nbMoveLast_Click);
            // 
            // nbDelete
            // 
            this.nbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.nbDelete, "nbDelete");
            this.nbDelete.Name = "nbDelete";
            this.nbDelete.Click += new System.EventHandler(this.nbDelete_Click);
            // 
            // nbClose
            // 
            this.nbClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.nbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nbClose.Image = global::MDM.Properties.Resources.close16;
            this.nbClose.Name = "nbClose";
            resources.ApplyResources(this.nbClose, "nbClose");
            this.nbClose.Click += new System.EventHandler(this.nbClose_Click);
            // 
            // nbEdit
            // 
            this.nbEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nbEdit.Image = global::MDM.Properties.Resources.edit;
            resources.ApplyResources(this.nbEdit, "nbEdit");
            this.nbEdit.Name = "nbEdit";
            this.nbEdit.Click += new System.EventHandler(this.nbEdit_Click);
            // 
            // nbWipe
            // 
            this.nbWipe.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.nbWipe, "nbWipe");
            this.nbWipe.Name = "nbWipe";
            this.nbWipe.Click += new System.EventHandler(this.nbWipe_Click);
            // 
            // nbUndelete
            // 
            this.nbUndelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nbUndelete.Image = global::MDM.Properties.Resources.yes;
            resources.ApplyResources(this.nbUndelete, "nbUndelete");
            this.nbUndelete.Name = "nbUndelete";
            this.nbUndelete.Click += new System.EventHandler(this.nbUndelete_Click);
            // 
            // nbFilter
            // 
            this.nbFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nbFilter.Image = global::MDM.Properties.Resources.filter;
            resources.ApplyResources(this.nbFilter, "nbFilter");
            this.nbFilter.Name = "nbFilter";
            // 
            // dataGrid
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGrid.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.dataGrid, "dataGrid");
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // dbpDataSet
            // 
            this.dbpDataSet.DataSetName = "DBDataSet";
            this.dbpDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // DBPanel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.dbpNavigator);
            this.Name = "DBPanel";
            ((System.ComponentModel.ISupportInitialize)(this.dbpNavigator)).EndInit();
            this.dbpNavigator.ResumeLayout(false);
            this.dbpNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbpDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingNavigator dbpNavigator;
        private System.Windows.Forms.ToolStripButton nbClose;
        private System.Windows.Forms.ToolStripButton nbMoveFirst;
        private System.Windows.Forms.ToolStripButton nbMovePrevious;
        private System.Windows.Forms.ToolStripTextBox nbPosition;
        private System.Windows.Forms.ToolStripLabel nbCount;
        private System.Windows.Forms.ToolStripButton nbMoveNext;
        private System.Windows.Forms.ToolStripButton nbMoveLast;
        private System.Windows.Forms.ToolStripButton nbAddNew;
        private System.Windows.Forms.ToolStripButton nbDelete;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.ToolStripButton nbEdit;
        private Data.DBDataSet dbpDataSet;
        private System.Windows.Forms.ToolStripButton nbWipe;
        private System.Windows.Forms.ToolStripButton nbUndelete;
        private System.Windows.Forms.ToolStripDropDownButton nbFilter;
    }
}
