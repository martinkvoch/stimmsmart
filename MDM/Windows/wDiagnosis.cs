using System;
using System.Data;
using System.Windows.Forms;
using System.Linq;

using MDM.Data;

namespace MDM.Windows
{
    public partial class wDiagnosis : Form
    {
        const string chnodeLab = "ChildNode";

        public int Id
        {
            get { return Convert.ToInt32(tvDiagnosis.SelectedNode.Tag); }
        }

        public string DgName
        {
            get { return tvDiagnosis.SelectedNode.Text; }
        }

        private TDiagnosis diagnosis = new TDiagnosis();
        public TDiagnosis Diagnosis
        {
            get { return diagnosis; }
            set
            {
                if(diagnosis != value)
                {
                    TreeNode node = tvDiagnosis.Nodes.Find(chnodeLab + value.ID.ToString(), true).FirstOrDefault();

                    if(node != null) tvDiagnosis.SelectedNode = node;
                    diagnosis = value;
                }
            }
        }

        public wDiagnosis()
        {
            InitializeComponent();
            fill();
        }

        private void fill()
        {
            using(Diagnosis dg = new Diagnosis())
            {
                const string selClm = "ID,NAME";

                foreach(DataRow row in dg.Select(selClm, string.Format("PARENT is null and not DELETED and LANG = '{0}'", Program.Language)).Rows)
                {
                    TreeNode node = new TreeNode(row[1].ToString());
                    DataTable childNodes = dg.Select(selClm, string.Format("PARENT = {0} and not DELETED and LANG = '{1}'", Convert.ToInt32(row[0]), Program.Language));

                    node.Tag = row[0];
                    node.Name = "Node" + node.Tag.ToString();
                    foreach(DataRow child in childNodes.Rows)
                    {
                        TreeNode childNode = new TreeNode(child[1].ToString());

                        childNode.Tag = child[0];
                        childNode.Name = chnodeLab + childNode.Tag.ToString();
                        node.Nodes.Add(childNode);
                    }
                    tvDiagnosis.Nodes.Add(node);
                }
                tvDiagnosis.SelectedNode = tvDiagnosis.Nodes[0].FirstNode;
                tvDiagnosis.ExpandAll();
            }
        }

        private void tvDiagnosis_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(e.Node.GetNodeCount(false) > 0) tvDiagnosis.SelectedNode = e.Node.FirstNode;
            else
            {
                int id = Convert.ToInt32(e.Node.Tag);

                using(Diagnosis dg = new Diagnosis())
                {
                    DataRow row = dg.Select("*", string.Format("ID = {0}", id)).Rows[0] as DataRow;

                    if(row != null) Diagnosis = new TDiagnosis(Convert.ToInt32(row["ID"]), row["NAME"].ToString(), Program.Language);
                }
            }
        }
    }
}
