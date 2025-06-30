using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganizationTreeForm.View
{
    public partial class PlayerControl : UserControl
    {
        public PlayerControl()
        {
            InitializeComponent();
        }

        // 선수 정보 설정 메서드
        public void SetPlayerInfo(string name, string number, string position, string foot, string imagePath)
        {
            NameResultLB.Text = name;
            NumberResultLB.Text = number;
            PositionResultLB.Text = position;
            FootResultLB.Text = foot;
            PBFace.ImageLocation = imagePath;

            PBFace.SizeMode = PictureBoxSizeMode.Zoom;
        }
    }
}
