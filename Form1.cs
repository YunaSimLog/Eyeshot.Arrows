using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eyeshot.Arrows
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            design1.ActiveViewport.Grid.Max = new Point2D(400, 200);
            design1.ActiveViewport.Grid.Step = 10;

            int arrowCount = 13;
            int arcRadius = 100;

            double arcSpan = 120;

            // 호 요소 넣기
            design1.Entities.Add(
                new Arc(Point3D.Origin, arcRadius, Utility.DegToRad(340), Utility.DegToRad(340 + 150)),
                Color.OrangeRed);

            // 화살표 넣기
            for (int i = 0; i < arrowCount; i++)
            {
                double radAngle = Utility.DegToRad(arcSpan * i / arrowCount);

                Mesh m = Mesh.CreateArrow(4, 100 - i * 4, 8, 24, 16, Mesh.natureType.Smooth);
                m.EdgeStyle = Mesh.edgeStyleType.Sharp;

                // 이동
                Translation tra = new Translation(arcRadius * Math.Cos(radAngle), arcRadius * Math.Sin(radAngle), 0);

                // 회전 
                Rotation rot = new Rotation(radAngle, Vector3D.AxisZ);

                Transformation combined = tra * rot;

                m.TransformBy(combined);

                design1.Entities.Add(m, Color.FromArgb(120 + i * 10, 255 - i * 10, 0));
            }

            design1.SetView(devDept.Eyeshot.viewType.Isometric);

            design1.ZoomFit();

            base.OnLoad(e);
        }

    }
}
