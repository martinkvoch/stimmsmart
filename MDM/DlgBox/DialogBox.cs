using LANlib;
using System.Windows.Forms;

namespace MDM.DlgBox
{
    public static class DialogBox
    {
        public static DialogResult ShowYN(string msg, string hdr)
        {
            DialogResult res = DialogResult.No;

            using(wDialogBoxYN box = new wDialogBoxYN())
            {
                box.Text = hdr;
                box.Message = msg;
                box.ShowDialog();
                res = box.Result;
            }
            return res;
        }

        private static wDialogBoxOK show(string msg, string hdr, MessageBoxIcon icon)
        {
            wDialogBoxOK res = new wDialogBoxOK();

            res.Text = hdr;
            res.Message = msg;
            res.SetIcon(icon);
            res.Show();
            res.Refresh();
            return res;
        }

        public static wDialogBoxOK ShowInfo(string msg, string hdr)
        {
            return show(msg, hdr, MessageBoxIcon.Information);
        }

        public static wDialogBoxOK ShowWarn(string msg, string hdr)
        {
            return show(msg, hdr, MessageBoxIcon.Warning);
        }

        public static wDialogBoxOK ShowError(string msg, string hdr)
        {
            Sound.Beep();
            return show(msg, hdr, MessageBoxIcon.Error);
        }
    }
}
