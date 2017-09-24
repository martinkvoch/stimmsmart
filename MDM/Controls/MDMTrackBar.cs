using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MDM.Controls
{
    public partial class MDMTrackBar : Control
    {
        private int value = byte.MinValue;
        private Color thumbColor = Color.DarkMagenta;
        private EventHandler onValueChanged;

        public event EventHandler ValueChanged
        {
            add
            {
                onValueChanged += value;
            }
            remove
            {
                onValueChanged -= value;
            }
        }

        protected virtual void OnValueChanged(EventArgs e)
        {
            onValueChanged?.Invoke(this, e);
        }

        [Category("Appearance")]
        [Description("Velikost nastaveného proudu v intervalu 0 - 255.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(0)]
        public int Value
        {
            get { return (byte)value; }
            set
            {
                int val = value < byte.MinValue ? byte.MinValue : value;

                val = val > byte.MaxValue ? byte.MaxValue : val;
                if(this.value != val)
                {
                    this.value = val;
                    OnValueChanged(EventArgs.Empty);
                    Invalidate();
                }
            }
        }

        [Category("Appearance")]
        [Description("Barva jezdce trackbaru.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(0xFF8B008B)]
        public Color ThumbColor
        {
            get { return thumbColor; }
            set { thumbColor = value; }
        }

        public MDMTrackBar()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            Rectangle r = pe.ClipRectangle;
            int indent = (r.Width / 3) + 4, top = r.Bottom - (int)Math.Round((Value / 255D) * (r.Height - indent), 0);

            base.OnPaint(pe);
            g.FillRectangle(new SolidBrush(BackColor), r);
            g.FillRectangle(new SolidBrush(ForeColor), r.Width / 4, top, r.Width / 2, r.Bottom - top);
            g.FillPolygon(new SolidBrush(ThumbColor), new Point[] { new Point(r.Width / 6, top - 4), new Point(r.Width / 2, top - indent), new Point(r.Width * 5 / 6, top - 4) });
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool res = true;
            const int WM_KEYDOWN = 0x0100, WM_SYSKEYDOWN = 0x0104;

            if((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                switch(keyData)
                {
                    case Keys.Up: Value++; break;
                    case Keys.Down: Value--; break;
                    default:
                        res = base.ProcessCmdKey(ref msg, keyData);
                        break;
                }
            }
            return res;
        }
    }
}
