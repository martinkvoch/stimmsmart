using System.Windows.Forms;

namespace MDM.DlgBox
{
    public static class DialogBox
    {
        public static DialogResult ShowYN(string msg, string hdr)
        {
            DialogResult res = DialogResult.No;
            wDialogBoxYN box = new wDialogBoxYN();

            box.Text = hdr;
            box.Message = msg;
            box.ShowDialog();
            res = box.Result;
            return res;
        }

        public static void ShowInfo(string msg, string hdr, MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            wDialogBoxOK box = new wDialogBoxOK();

            box.Text = hdr;
            box.Message = msg;
            box.SetIcon(icon);
            box.ShowDialog();
        }

        public static void ShowWarn(string msg, string hdr, MessageBoxIcon icon = MessageBoxIcon.Warning)
        {
            wDialogBoxOK box = new wDialogBoxOK();

            box.Text = hdr;
            box.Message = msg;
            box.SetIcon(icon);
            box.ShowDialog();
        }

        public static void ShowError(string msg, string hdr, MessageBoxIcon icon = MessageBoxIcon.Error)
        {
            wDialogBoxOK box = new wDialogBoxOK();

            box.Text = hdr;
            box.Message = msg;
            box.SetIcon(icon);
            box.ShowDialog();
        }
    }
}
