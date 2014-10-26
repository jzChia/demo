using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Security.Principal;
using System.Threading;
using Demo.Entities;

namespace Demo
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm1()
        {
            InitializeComponent();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

       private void simpleButton1_Click(object sender, EventArgs e)
        {
            string userName = textEdit1.Text;
            string pwd = textEdit2.Text;
            if (String.IsNullOrWhiteSpace(userName))
            {
                MessageBox.Show("请输入用户名!");
                textEdit1.Focus();
                return;
            }


            if (String.IsNullOrWhiteSpace(pwd))
            {
                MessageBox.Show("请输入密码!");
                textEdit2.Focus();
                return;
            }

            UserInfo user;
            Role role;
            int result = UserInfo.UserInfoLogin(userName, pwd, out user);

            if (result == 1)
            {
                MessageBox.Show("登录失败!");
                textEdit1.Focus();
                return;
            }
            else if(result==2)
            {
                MessageBox.Show("用户停用!");
                textEdit1.Focus();
                return;
            }
            else if (result == 3)
            {
                MessageBox.Show("用户已登陆!");
                textEdit1.Focus();
                return;
            }
            else if (result == 4)
            {
                MessageBox.Show("未授权用户!");
                textEdit1.Focus();
                return;
            }
            else if (result == 0)
            { }
            else
            {
                MessageBox.Show("未知原因!");
                textEdit1.Focus();
                return;
            }

            if (user != null)
            {
                role = Role.GetRoleById(user.ROLEID);
                if (role == null)
                {
                    MessageBox.Show("未知角色!");
                    textEdit1.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("用户不存在!");
                textEdit1.Focus();
                return;
            }

            IIdentity _identity = new GenericIdentity(userName);
            IPrincipal _principal = new CurrentUser(_identity, new string[] { "管理员" }, role.QUERYGRADER,role.GETGRADER);

            Thread.CurrentPrincipal = _principal;//将其附加到当前线程的CurrentPrincipal

            MainForm mf = new MainForm(1);
            if (mf.ShowDialog() == DialogResult.OK)
            {
                this.Hide();
            }

            //this.Hide();
            //if (textEdit1.Text == "user1") //普通用户
            //{
            //    MainForm mf = new MainForm(1);
            //    if (mf.ShowDialog() == DialogResult.OK) 
            //    {
            //        this.Hide();
            //    }
            //}
            //else if (textEdit1.Text.Equals("user2"))//数据管理员
            //{
            //    MainForm mf = new MainForm(2);
            //    if (mf.ShowDialog() == DialogResult.OK)
            //    {
            //        this.Hide();
            //    }
            //}
            //else if (textEdit1.Text.Equals("user3"))//系统管理员
            //{
            //    MainForm mf = new MainForm(3);
            //    if (mf.ShowDialog() == DialogResult.OK)
            //    {
            //        this.Hide();
            //    }
            //}
        }

        private void XtraForm1_MouseUp(object sender, MouseEventArgs e)
        {
            allowdrop = false; 
        }

        private void XtraForm1_MouseDown(object sender, MouseEventArgs e)
        {
            allowdrop = true;
            mypoint = new Point(-e.X, -e.Y);
        }

        private bool allowdrop = false;
        private Point mypoint = new Point();

        private void XtraForm1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point myposition = Control.MousePosition;
                myposition.Offset(mypoint.X, mypoint.Y);
                this.DesktopLocation = myposition;
            }
        }
    }
}