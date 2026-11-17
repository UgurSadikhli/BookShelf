using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using File = System.IO.File;


namespace BookShelf
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            LoadUser();
            LoadProduct();
            LoadClient();
            LoadOrder();


            InitializeComponent();
            panel1.Dock = DockStyle.Fill;
            panel2.Dock = DockStyle.Fill;
            panel3.Dock = DockStyle.Fill;
            student_panel4.Dock = DockStyle.None;
            Book_panel4.Dock = DockStyle.None;
            Order_panel5.Dock = DockStyle.None;
            panel2.Hide();
            panel3.Hide();
            student_panel4.Hide();
            Book_panel4.Hide();
            Order_panel5.Hide();
            groupBox1.Visible = false;
            label13.Visible = false;
        }
        public List<int> lis = new List<int>();

        XmlSerializer svusers = new XmlSerializer(typeof(Librarian_List));
        XmlSerializer svusers4 = new XmlSerializer(typeof(Order_List));
        XmlSerializer svusers3 = new XmlSerializer(typeof(Student_List));
        XmlSerializer svusers2 = new XmlSerializer(typeof(Book_List));


        Librarian librarian = new Librarian();
        Student student = new Student();
        Book book = new Book();
        Order order = new Order();
        Librarian_List librarian_List = new Librarian_List();
        Order_List order_list = new Order_List();
        Book_List bookt_list = new Book_List();
        Student_List student_list = new Student_List();

        string broom_sweeps = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "audio", "broom-sweeps-1.mp3");
        string mixtic_game = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "audio", "mixkit-game-ball-tap-2073.mp3");
        string mixtic_correct = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "audio", "mixkit-correct-answer-tone-2870.mp3");
        string short_succes = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "audio", "short-success-sound-glockenspiel-treasure-video-game-6346.mp3");
        string path_erro = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "audio", "erro.mp3");
        string classik_mus = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "audio", "sb_adriftamonginfinitestars(chosic.com).mp3");
        string no_sound = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "audio", "icons8-no-sound-100.png");
        string sound = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "audio", "icons8-sound-100.png");
        string send = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "audio", "icons8-send-90(1).png");
        string sending = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "audio", "icons8-send-90.png");
        string ErrPhoto = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "audio", "icons8-email-send-90.png");
        bool res = false;

        public void SaveUser()
        {
            using (Stream fStream = File.Create("../../../../librarians.txt"))
            {
                svusers.Serialize(fStream, librarian_List);
            }
        }
        public void LoadUser()
        {
            using (Stream fStream = File.OpenRead("../../../../librarians.txt"))
            {
                librarian_List = (Librarian_List)svusers.Deserialize(fStream);
            }
        }
        public void SaveProduct()
        {
            using (Stream fStream = File.Create("../../../../books.txt"))
            {
                svusers2.Serialize(fStream, bookt_list);
            }
        }
        public void LoadProduct()
        {
            using (Stream fStream = File.OpenRead("../../../../books.txt"))
            {
                bookt_list = (Book_List)svusers2.Deserialize(fStream);
            }
        }
        public void SaveClient()
        {
            using (Stream fStream = File.Create("../../../../students.txt"))
            {
                svusers3.Serialize(fStream, student_list);
            }
        }
        public void LoadClient()
        {
            using (Stream fStream = File.OpenRead("../../../../students.txt"))
            {
                student_list = (Student_List)svusers3.Deserialize(fStream);
            }
        }

        public void SaveOrder()
        {
            using (Stream fStream = File.Create("../../../../orders.txt"))
            {
                svusers4.Serialize(fStream, order_list);
            }
        }
        public void LoadOrder()
        {
            using (Stream fStream = File.OpenRead("../../../../orders.txt"))
            {
                order_list = (Order_List)svusers4.Deserialize(fStream);
            }
        }
        int rand()
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int num = rnd.Next(100000, 900000);
            return num;
        }

        int OrderRandomID()
        {
            int a = 0;
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int num = rnd.Next(1000000, 9000000);
            foreach (var item in order_list.orders)
            {
                if (item.order_id != num)
                {
                    a = 0;
                }
                else
                {
                    a = 1;
                }
            }
            if (a == 0)
            {
                return num;
            }
            else
            {
                return OrderRandomID();
            }
        }

        void SMTPrNew()
        {
            int a = rand();
            lis.Add(a);
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential(
                "andrewmart1545@gmail.com",
                "chkojvgmrlorkvml");

            var message = new System.Net.Mail.MailMessage()
            {
                Subject = $"Your verification code {textBox6.Text}",
                Body = a.ToString()
            };

            message.From = new System.Net.Mail.MailAddress("andrewmart1545@gmail.com", "BookShelf");
            message.To.Add(new System.Net.Mail.MailAddress(textBox5.Text));

            client.Send(message);
        }

        void SMTPr2()
        {
            int a = rand();
            lis.Add(a);
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential(
                "andrewmart1545@gmail.com",
                "chkojvgmrlorkvml");

            var message = new System.Net.Mail.MailMessage()
            {
                Subject = $"Your verification code {librarian.name}",
                Body = a.ToString()
            };

            message.From = new System.Net.Mail.MailAddress("andrewmart1545@gmail.com", "BookShelf");
            message.To.Add(new System.Net.Mail.MailAddress(textBox22.Text));

            client.Send(message);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = mixtic_game;
            panel2.Show();
            panel1.Hide();
        }



        private void pictureBox6_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = mixtic_game;
            panel2.Hide();
            panel1.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = mixtic_game;
            int check = 0;
            foreach (var combi in librarian_List.librarians)
            {
                if (combi.username == textBox1.Text && combi.password == textBox2.Text)
                {
                    librarian.username = combi.username;
                    librarian.password = combi.password;
                    librarian.email = combi.email;
                    librarian.name = combi.name;
                    panel1.Hide();
                    panel3.Show();
                    check++;
                }
            }
            if (check == 0)
            {
                axWindowsMediaPlayer1.URL = path_erro;
                MessageBox.Show("No Such user");

            }

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = mixtic_game;
            lis.Clear();
            int a = 0;
            foreach (var c in librarian_List.librarians)
            {
                if (textBox4.Text == c.username)
                {
                    a++;
                }
            }
            if (textBox4.Text != "" && textBox3.Text != "" && textBox6.Text != "" && textBox5.Text.Contains("@") && a == 0)
            {
                librarian.username = textBox4.Text;
                librarian.password = textBox3.Text;
                Thread thread = new Thread(SMTPrNew);
                thread.Start();
                MessageBox.Show("code has been sent");
                label11.Visible = true;
                maskedTextBox1.Visible = true;
                pictureBox11.Visible = true;
            }
            else
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                string title = "Error";
                DialogResult result = MessageBox.Show("Retry again some sing gone wrong\n **fill all gaps\n **maybe there is user with your username (try to change it)", title, buttons);
            }
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Code has been sent to your email '{textBox5.Text}' \n Insert it correctly to create accaunt");
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            try
            {
                if (maskedTextBox1.Text == lis.First().ToString())
                {
                    MessageBox.Show("SUCSESSFULY DONE");
                    int i = 0;

                    librarian.email = textBox5.Text;
                    librarian.name = textBox6.Text;
                    librarian_List.librarians.Add(new Librarian { username = librarian.username, password = librarian.password, email = librarian.email, name = librarian.name });
                    SaveUser();
                    panel2.Hide();
                    textBox3.Text = null;
                    lis.Clear();
                    textBox4.Text = null;
                    textBox5.Text = null;
                    textBox6.Text = null;
                    maskedTextBox1.Text = null;
                    maskedTextBox1.Visible = false;
                    label11.Visible = false;
                    pictureBox11.Visible = false;
                    panel1.Show();
                    lis.Clear();

                }

            }
            catch (Exception ex) { }
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {

            redownload(3);

        }

        private void showToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            redownload(1);


        }

        private void showToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            redownload(2);

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            df = false;
            get = false;
            groupBox1.Visible = true;
            groupBox3.Visible = false;
            groupBox2.Visible = false;

            label13.Visible = true;
            student_panel4.Show();
            Order_panel5.Hide();
            Book_panel4.Hide();
            label13.Text = "Student";
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            label13.Visible = true;
            groupBox3.Visible = false;
            groupBox2.Visible = false;
            Order_panel5.Show();
            student_panel4.Hide();
            Book_panel4.Hide();
            label13.Text = "Order";
            df = false;
            get = false;
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            textBox43.Text = "no";
            foreach (var user in student_list.students)
            {
                if (user.owner_name == librarian.username)
                {
                    comboBox2.Items.Add(user.name);
                }
            }
            foreach (var book in bookt_list.books)
            {
                comboBox3.Items.Add(book.Name);
            }


        }

        private void addToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            df = false;
            get = false; groupBox1.Visible = true;
            label13.Visible = true;
            groupBox3.Visible = false;
            groupBox2.Visible = false;
            Book_panel4.Show();
            Order_panel5.Hide();
            student_panel4.Hide();
            label13.Text = "Book";
        }

        private void changeUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel3.Hide();
            panel1.Show();
            librarian.username = null;
            librarian.password = null;
            librarian.email = null;
            librarian.name = null;
            groupBox1.Visible = false;
            label13.Visible = false;
            Book_panel4.Hide();
            Order_panel5.Hide();
            student_panel4.Hide();
            listView_Book.Visible = false;
            listView_Borrowed.Visible = false;
            listView_Student.Visible = false;

        }

        private void closeAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveClient();
            SaveOrder();
            SaveProduct();
            SaveUser();
            Application.Exit();
        }

        void redownload(int i) //1-book || 2-borrowed || 3-student
        {


            switch (i)
            {
                case 1:
                    {
                        listView_Student.Visible = false;
                        listView_Borrowed.Visible = false;
                        listView_Book.Visible = true;
                        listView_Book.Items.Clear();
                        foreach (var cli in bookt_list.books)
                        {
                            ListViewItem lvi = new ListViewItem(cli.Name);
                            lvi.SubItems.Add(cli.Quantitiy.ToString());
                            lvi.SubItems.Add(cli.Describtion);
                            listView_Book.Items.Add(lvi);
                        }
                        break;
                    }
                case 2:
                    {
                        listView_Borrowed.Visible = true;
                        listView_Student.Visible = false;
                        listView_Book.Visible = false;
                        listView_Borrowed.Items.Clear();
                        foreach (var cli in order_list.orders)
                        {
                            string a = null;
                            if (cli.owner_name == librarian.username)
                            {
                                if (cli.restored == true)
                                {
                                    a = "yes";
                                }
                                else { a = "no"; }
                                ListViewItem lvi = new ListViewItem(cli.student_name);
                                lvi.SubItems.Add(cli.quantity.ToString());
                                lvi.SubItems.Add(cli.borrowed.ToString());
                                lvi.SubItems.Add(cli.book_name);
                                lvi.SubItems.Add(cli.returnal_time.ToString());
                                lvi.SubItems.Add(a);
                                lvi.SubItems.Add(cli.order_id.ToString());
                                listView_Borrowed.Items.Add(lvi);
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        listView_Borrowed.Visible = false;
                        listView_Book.Visible = false;
                        listView_Student.Visible = true;
                        listView_Student.Items.Clear();
                        foreach (var cli in student_list.students)
                        {
                            if (cli.owner_name == librarian.username)
                            {
                                ListViewItem lvi = new ListViewItem(cli.name);
                                lvi.SubItems.Add(cli.email);
                                lvi.SubItems.Add(cli.phone.ToString());
                                listView_Student.Items.Add(lvi);
                            }
                        }
                        break;
                    }

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (label13.Text == "Order")
                {

                    if (textBox18.Text == "" || textBox18.Text == "empty")
                    {
                        textBox18.Text = "empty";
                    }
                    if (comboBox2.SelectedItem == null || comboBox2.Text == "empty")
                    {
                        comboBox2.Text = "empty";
                    }
                    if (comboBox3.SelectedItem == null || comboBox3.Text == "empty")
                    {
                        comboBox3.Text = "empty";
                    }
                    else
                    {
                        order.book_name = comboBox3.SelectedItem.ToString();
                        order.student_name = comboBox2.SelectedItem.ToString();
                        order.quantity = Int32.Parse(textBox18.Text);
                        order.borrowed = dateTimePicker1.Value;
                        order.returnal_time = dateTimePicker2.Value;
                        order.order_id = OrderRandomID();
                        order.restored = false;
                        foreach (var i in bookt_list.books)
                        {
                            if (i.Name == order.book_name)
                            {
                                if (i.Quantitiy >= order.quantity)
                                {
                                    i.Quantitiy -= order.quantity;
                                    SaveProduct();
                                    listView_Book.Items.Clear();
                                    foreach (var cli in bookt_list.books)
                                    {
                                        ListViewItem lvi = new ListViewItem(cli.Name);
                                        lvi.SubItems.Add(cli.Quantitiy.ToString());
                                        lvi.SubItems.Add(cli.Describtion);
                                        listView_Book.Items.Add(lvi);
                                    }


                                    order_list.orders.Add(new Order { book_name = order.book_name, quantity = order.quantity, owner_name = librarian.username, restored = order.restored, returnal_time = order.returnal_time, borrowed = order.borrowed, student_name = order.student_name, order_id = order.order_id });
                                    SaveOrder();

                                    comboBox3.Text = "";
                                    comboBox2.Text = "";
                                    textBox43.Text = "";
                                    textBox18.Text = "";
                                    int num = 2;
                                    Thread thread = new Thread(() => redownload(num));
                                    thread.Start();
                                    axWindowsMediaPlayer1.URL = short_succes;

                                }
                                else
                                {
                                    axWindowsMediaPlayer1.URL = path_erro;
                                    MessageBox.Show("Thre is not as much book");

                                }
                            }
                        }

                    }
                }

                else if (label13.Text == "Book")
                {
                    if (textBox12.Text == "" || textBox12.Text == "empty")
                    {
                        textBox12.Text = "empty";
                    }
                    if (textBox14.Text == "" || textBox14.Text == "empty")
                    {
                        textBox14.Text = "empty";
                    }
                    if (textBox13.Text == "0" || textBox13.Text == "empty")
                    {
                        textBox13.Text = "empty";
                    }
                    else
                    {
                        book.Name = textBox14.Text;
                        book.Quantitiy = Int32.Parse(textBox13.Text);
                        book.Describtion = textBox12.Text;
                        bookt_list.books.Add(new Book { Name = book.Name, Quantitiy = book.Quantitiy, Describtion = book.Describtion });
                        SaveProduct();
                        textBox14.Text = "";
                        textBox13.Text = "0";
                        textBox12.Text = "";

                        redownload(1);
                        axWindowsMediaPlayer1.URL = short_succes;
                    }
                }

                else if (label13.Text == "Student")
                {
                    if (textBox7.Text == "" || textBox7.Text == "empty")
                    {
                        textBox7.Text = "empty";
                    }
                    if (textBox10.Text == "" || textBox10.Text == "empty")
                    {
                        textBox10.Text = "empty";
                    }
                    if (textBox11.Text == "" || textBox11.Text == "empty")
                    {
                        textBox11.Text = "empty";
                    }
                    else
                    {
                        student.name = textBox7.Text;
                        student.phone = Int32.Parse(textBox10.Text);
                        student.email = textBox11.Text;
                        student_list.students.Add(new Student { name = student.name, email = student.email, phone = student.phone, owner_name = librarian.username });
                        SaveClient();

                        textBox7.Text = "";
                        textBox10.Text = "";
                        textBox11.Text = "";
                        redownload(3);
                        axWindowsMediaPlayer1.URL = short_succes;
                    }
                }

            }
            catch (Exception ex) { }


        }

        bool get = false;
        private void otherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;

            groupBox3.Visible = false;
            df = false;

            if (get == false)
            {
                label26.Text = "SETTINGS";
                groupBox2.Visible = true;
                textBox17.Text = librarian.username;
                textBox19.Text = librarian.name;
                textBox22.Text = librarian.email;
                textBox23.Text = librarian.password;
                get = true;
            }
            else if (get == true)
            {

                label26.Text = "SETTINGS";
                groupBox2.Visible = false;
                textBox17.Text = librarian.username;
                textBox19.Text = librarian.name;
                textBox22.Text = librarian.email;
                textBox23.Text = librarian.password;
                get = false;

            }
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox22.Text != librarian.email)
            {
                lis.Clear();
                Thread thread = new Thread(SMTPr2);
                thread.Start();
                MessageBox.Show("code has been sent");
                textBox41.Visible = true;
                pictureBox43.Visible = true;
                label43.Visible = true;
            }
            else if (textBox22.Text == librarian.email)
            {
                int ee = 0;
                foreach (var lib in librarian_List.librarians)
                {
                    if (textBox17.Text != librarian.username && textBox17.Text == lib.username)
                    {
                        MessageBox.Show("Username already exists");
                        ee++;
                    }
                }
                foreach (var lib in librarian_List.librarians)
                {
                    if (lib.username == librarian.username && ee == 0)
                    {
                        lib.username = textBox17.Text;
                        lib.name = textBox19.Text;
                        lib.email = textBox22.Text;
                        lib.password = textBox23.Text;
                        librarian.username = lib.username;
                        librarian.name = lib.name;
                        librarian.email = lib.email;
                        librarian.password = lib.password;
                        SaveUser();

                        MessageBox.Show("SUCSESSFULY DONE");
                    }
                }
            }
        }

        private void pictureBox30_Click(object sender, EventArgs e)
        {
            textBox17.Text = librarian.username;
            textBox19.Text = librarian.name;
            textBox22.Text = librarian.email;
            textBox23.Text = librarian.password;
            textBox41.Visible = false;
            pictureBox43.Visible = false;
            label43.Visible = false;

        }
        bool df = false;
        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = mixtic_game;
            groupBox2.Visible = false;
            groupBox1.Visible = false;
            get = false;
            if (df == false)
            {
                groupBox3.Visible = true;
                df = true;
                listView_Book.Visible = false;
                listView_Borrowed.Visible = true;
                listView_Student.Visible = false;
                redownload(2);
            }
            else if (df == true)
            {
                groupBox3.Visible = false;
                df = false;
            }


        }

        private void listView_Borrowed_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBox39.Text = listView_Borrowed.SelectedItems[0].SubItems[0].Text;
                textBox35.Text = listView_Borrowed.SelectedItems[0].SubItems[0].Text;
                textBox36.Text = listView_Borrowed.SelectedItems[0].SubItems[3].Text;
                textBox30.Text = listView_Borrowed.SelectedItems[0].SubItems[1].Text;
                textBox37.Text = listView_Borrowed.SelectedItems[0].SubItems[5].Text;
                if (textBox37.Text == "yes") { checkBox1.Checked = true; } else { checkBox1.Checked = false; }
                textBox33.Text = listView_Borrowed.SelectedItems[0].SubItems[2].Text;
                textBox34.Text = listView_Borrowed.SelectedItems[0].SubItems[4].Text;
                textBox42.Text = listView_Borrowed.SelectedItems[0].SubItems[6].Text;
                pictureBox45.Enabled = true;
            }
            catch (Exception ex) { }
        }



        private void pictureBox39_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox37.Text == "yes")
                {
                    foreach (var i in order_list.orders.ToList())
                    {
                        if (i.order_id == Int32.Parse(textBox42.Text))
                        {
                            order_list.orders.Remove(i);
                        }
                    }
                    SaveOrder();
                    listView_Borrowed.Items.Remove(listView_Borrowed.SelectedItems[0]);
                    redownload(2);
                }
                else
                {
                    MessageBox.Show("Books were't returned", "Return books",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("Please select in row to delete", "Select",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void textBox38_TextChanged(object sender, EventArgs e)
        {
            string f = textBox38.Text;
            string a = null;
            listView_Borrowed.Items.Clear();
            foreach (var u in order_list.orders)
            {
                if (u.student_name.Contains(f) || u.book_name.Contains(f) || u.quantity.ToString().Contains(f))
                {
                    if (u.owner_name == librarian.username)
                    {
                        if (u.restored == true)
                        {
                            a = "yes";
                        }
                        else { a = "no"; }
                        ListViewItem lvi = new ListViewItem(u.student_name);
                        lvi.SubItems.Add(u.quantity.ToString());
                        lvi.SubItems.Add(u.borrowed.ToString());
                        lvi.SubItems.Add(u.book_name);
                        lvi.SubItems.Add(u.returnal_time.ToString());
                        lvi.SubItems.Add(a);
                        listView_Borrowed.Items.Add(lvi);
                    }
                }
            }
        }

        bool net = false;
        private void filterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = mixtic_game;
            groupBox5.Visible = false;
            groupBox6.Visible = false;
            rr = false;
            uu = false;
            if (net == false)
            {
                groupBox4.Visible = true;
                net = true;
                redownload(2);
            }
            else
            {
                groupBox4.Visible = false;
                net = false;
            }
        }





        //email coding border
        void EmailNumber_Of_Students(int student)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential(
                "andrewmart1545@gmail.com",
                "chkojvgmrlorkvml");

            var message = new System.Net.Mail.MailMessage()
            {
                Subject = $"Dear, {librarian.name}",
                Body = $"You have {student.ToString()} students"

            };

            message.From = new System.Net.Mail.MailAddress("andrewmart1545@gmail.com", "BookShelf");
            message.To.Add(new System.Net.Mail.MailAddress(librarian.email));

            client.Send(message);
        }

        void OverallBooksLent(int Book)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential(
                "andrewmart1545@gmail.com",
                "chkojvgmrlorkvml");

            var message = new System.Net.Mail.MailMessage()
            {
                Subject = $"Dear, {librarian.name}",
                Body = $"You have {Book.ToString()} books lent"

            };

            message.From = new System.Net.Mail.MailAddress("andrewmart1545@gmail.com", "BookShelf");
            message.To.Add(new System.Net.Mail.MailAddress(librarian.email));

            client.Send(message);
        }
        void NumberOfLentedPeople(int order, string state)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential(
                "andrewmart1545@gmail.com",
                "chkojvgmrlorkvml");

            var message = new System.Net.Mail.MailMessage()
            {
                Subject = $"Dear, {librarian.name}",
                Body = $"You have {order.ToString()} {state} orders"

            };

            message.From = new System.Net.Mail.MailAddress("andrewmart1545@gmail.com", "BookShelf");
            message.To.Add(new System.Net.Mail.MailAddress(librarian.email));

            client.Send(message);
        }


        void StudentEmailInfo(int quan, string book, string returnaltime, string email, string name)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential(
                "andrewmart1545@gmail.com",
                "chkojvgmrlorkvml");

            var message = new System.Net.Mail.MailMessage()
            {
                Subject = $"Hello, {name}",
                Body = $"Pelase return '{book}' book by '{returnaltime}'\nAs reminding you have '{quan}' book(s)"

            };

            message.From = new System.Net.Mail.MailAddress("andrewmart1545@gmail.com", "BookShelf");
            message.To.Add(new System.Net.Mail.MailAddress(email));

            client.Send(message);
        }
        void StudentEmailInfoComment(int quan, string book, string returnaltime, string email, string name, string comment)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential(
                "andrewmart1545@gmail.com",
                "chkojvgmrlorkvml");

            var message = new System.Net.Mail.MailMessage()
            {
                Subject = $"Hello, {name}",
                Body = $"Pelase return '{book}' book by '{returnaltime}'\nAs reminding you have '{quan}' book(s)\n\nMr(s) {librarian.name}'s comment:\n⋅{comment}"

            };

            message.From = new System.Net.Mail.MailAddress("andrewmart1545@gmail.com", "BookShelf");
            message.To.Add(new System.Net.Mail.MailAddress(email));

            client.Send(message);
        }
        private void numberOfStuentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (res == true)
            {
                int num = 0;
                foreach (var student in student_list.students)
                {
                    if (student.owner_name == librarian.username)
                    {
                        num++;
                    }
                }
                Thread thread = new Thread(() => EmailNumber_Of_Students(num));
                thread.Start();
                axWindowsMediaPlayer1.URL = mixtic_correct;
                MessageBox.Show("Email sucssfully sent");
            }
            else if (res == false)
            {
                axWindowsMediaPlayer1.URL = path_erro;
                string message = "\tEmail could't be sent \n\tWeek connection or not connected";
                string title = "Innet error";
                MessageBoxIcon ico = MessageBoxIcon.Error;
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons, ico);
            }
        }

        private void overallBooksLentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (res == true)
            {
                int num = 0;
                foreach (var student in order_list.orders)
                {
                    if (student.owner_name == librarian.username)
                    {
                        num += student.quantity;
                    }
                }
                Thread thread = new Thread(() => OverallBooksLent(num));
                thread.Start();
                axWindowsMediaPlayer1.URL = mixtic_correct;
                MessageBox.Show("Email sucssfully sent");
            }
            else if (res == false)
            {
                axWindowsMediaPlayer1.URL = path_erro;
                string message = "\tEmail could't be sent \n\tWeek connection or not connected";
                string title = "Innet error";
                MessageBoxIcon ico = MessageBoxIcon.Error;
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons, ico);
            }
        }

        private void finnishedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (res == true)
            {
                int num = 0;
                foreach (var student in order_list.orders)
                {
                    if (student.owner_name == librarian.username)
                    {
                        if (student.restored == true)
                        {
                            num++;
                        }
                    }
                }
                Thread thread = new Thread(() => NumberOfLentedPeople(num, "complete"));
                thread.Start();
                axWindowsMediaPlayer1.URL = mixtic_correct;
                MessageBox.Show("Email sucssfully sent");
            }
            else if (res == false)
            {
                axWindowsMediaPlayer1.URL = path_erro;
                string message = "\tEmail could't be sent \n\tWeek connection or not connected";
                string title = "Innet error";
                MessageBoxIcon ico = MessageBoxIcon.Error;
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons, ico);
            }
        }

        private void incompleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (res == true)
            {
                int num = 0;
                foreach (var student in order_list.orders)
                {
                    if (student.owner_name == librarian.username)
                    {
                        if (student.restored == false)
                        {
                            num++;
                        }
                    }
                }
                Thread thread = new Thread(() => NumberOfLentedPeople(num, "uncomplete"));
                thread.Start();
                axWindowsMediaPlayer1.URL = mixtic_correct;
                MessageBox.Show("Email sucssfully sent");
            }
            else if (res == false)
            {
                axWindowsMediaPlayer1.URL = path_erro;
                string message = "\tEmail could't be sent \n\tWeek connection or not connected";
                string title = "Innet error";
                MessageBoxIcon ico = MessageBoxIcon.Error;
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons, ico);
            }
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (res == true)
            {
                int num = 0;
                foreach (var student in order_list.orders)
                {
                    if (student.owner_name == librarian.username)
                    {
                        num++;
                    }
                }
                Thread thread = new Thread(() => NumberOfLentedPeople(num, "overall"));
                thread.Start();
                axWindowsMediaPlayer1.URL = mixtic_correct;
                MessageBox.Show("Email sucssfully sent");
            }
            else if (res == false)
            {
                axWindowsMediaPlayer1.URL = path_erro;
                string message = "\tEmail could't be sent \n\tWeek connection or not connected";
                string title = "Innet error";
                MessageBoxIcon ico = MessageBoxIcon.Error;
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons, ico);
            }
        }
        bool rr = false;

        private void pictureBox41_Click(object sender, EventArgs e)
        {
            PushSend();
        }

        private void PushSend()
        {


            if (res == true)
            {
                pictureBox41.Image = Image.FromFile(send);
                try
                {
                    string orderIDD = listView_Borrowed.SelectedItems[0].SubItems[6].Text;

                    foreach (var orders in order_list.orders)
                    {
                        if (orders.order_id == Int32.Parse(orderIDD))
                        {
                            string book = "";
                            int quan = 0;
                            string nameS = "";
                            string returnaltime = "";
                            string email = "";
                            string name = listView_Borrowed.SelectedItems[0].SubItems[0].Text;
                            foreach (var student in student_list.students)
                            {

                                if (orders.student_name == name && name == student.name)
                                {
                                    nameS = student.name;
                                    book = orders.book_name;
                                    quan = orders.quantity;
                                    returnaltime = orders.returnal_time.ToString();
                                    email = student.email;
                                }

                            }
                            axWindowsMediaPlayer1.URL = mixtic_correct;
                            if (groupBox6.Visible == false)
                            {
                                Thread thread = new Thread(() => StudentEmailInfo(quan, book, returnaltime, email, name));
                                thread.Start();
                                MessageBox.Show($"Email sucssfully sent to adress {email}");
                                pictureBox41.Image = Image.FromFile(sending);

                            }
                            else if (groupBox6.Visible == true)
                            {

                                Thread thread = new Thread(() => StudentEmailInfoComment(quan, book, returnaltime, email, name, textBox40.Text));
                                thread.Start();
                                MessageBox.Show($"Email sucssfully sent to adress {email}");
                                pictureBox41.Image = Image.FromFile(sending);


                            }
                        }
                    }
                }
                catch { }
            }
            else if (res == false)
            {
                axWindowsMediaPlayer1.URL = path_erro;
                pictureBox41.Image = Image.FromFile(ErrPhoto);
                string message = "\tEmail could't be sent \n\tWeek connection or not connected";
                string title = "Innet error";
                MessageBoxIcon ico = MessageBoxIcon.Error;
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons, ico);
            }

            pictureBox41.Image = Image.FromFile(sending);

        }
        private void pictureBox42_Click(object sender, EventArgs e)
        {
            try
            {
                axWindowsMediaPlayer1.URL = mixtic_game;
                string message = "Are you sure to clear desktop?";
                string title = "Clear";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    axWindowsMediaPlayer1.URL = broom_sweeps;
                    groupBox1.Visible = false;
                    groupBox2.Visible = false;
                    groupBox3.Visible = false;
                    groupBox4.Visible = false;
                    groupBox5.Visible = false;
                    listView_Book.Visible = false;
                    listView_Borrowed.Visible = false;
                    groupBox6.Visible = false;
                    listView_Student.Visible = false;
                }
                else if (result == DialogResult.No)
                {

                }
            }
            catch { }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            axWindowsMediaPlayer2.URL = classik_mus;
            timer1.Start();
            timer2.Start();

        }

        private void studentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = mixtic_game;
        }

        private void orderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = mixtic_game;
        }

        private void bookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = mixtic_game;
        }

        private void emailToolStripMenuItem_Click(object sender, EventArgs e)
        {

            axWindowsMediaPlayer1.URL = mixtic_game;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = mixtic_game;
        }

        public bool IsConnectedToInternet()
        {
            const string NCSI_TEST_URL = "http://www.msftncsi.com/ncsi.txt";
            const string NCSI_TEST_RESULT = "Microsoft NCSI";
            const string NCSI_DNS = "dns.msftncsi.com";
            const string NCSI_DNS_IP_ADDRESS = "131.107.255.255";

            try
            {

                var webClient = new WebClient();
                string result = webClient.DownloadString(NCSI_TEST_URL);
                if (result != NCSI_TEST_RESULT)
                {
                    res = false;
                    return false;
                }


                var dnsHost = Dns.GetHostEntry(NCSI_DNS);
                if (dnsHost.AddressList.Count() < 0 || dnsHost.AddressList[0].ToString() != NCSI_DNS_IP_ADDRESS)
                {
                    res = false;
                    return false;

                }
            }
            catch (Exception ex)
            {

                res = false;
                return false;

            }

            res = true;
            return true;

        }
        private void regularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                groupBox6.Visible = false;
                listView_Book.Visible = false;
                listView_Borrowed.Visible = true;
                listView_Student.Visible = false;
                redownload(2);
                groupBox4.Visible = false;
                net = false;
                uu = false;
                if (rr == false)
                {
                    groupBox5.Visible = true;
                    rr = true;
                }
                else if (rr == true)
                {
                    groupBox5.Visible = false;
                    rr = false;
                }

            }
            catch { }

        }
        bool uu = false;
        private void specialComentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView_Book.Visible = false;
            listView_Borrowed.Visible = true;
            listView_Student.Visible = false;
            redownload(2);
            groupBox4.Visible = false;
            net = false;
            rr = false;
            if (uu == false)
            {
                groupBox5.Visible = true;
                groupBox6.Visible = true;
                uu = true;
            }
            else if (uu == true)
            {
                groupBox5.Visible = false;

                groupBox6.Visible = false;
                uu = false;
            }
        }



        private void groupBox1_VisibleChanged(object sender, EventArgs e)
        {
            get = false;
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            label41.Text = "";
            label42.Text = "";

            label41.Text = DateTime.Now.ToString("HH:mm");
            label42.Text = DateTime.Now.ToString("dd dddd");

        }

        private void textBox41_TextChanged(object sender, EventArgs e)
        {
            if (textBox41.Text == lis.First().ToString())
            {

                foreach (var lib in librarian_List.librarians)
                {
                    if (lib.username == librarian.username)
                    {
                        lib.email = textBox22.Text;
                        librarian.email = lib.email;

                        SaveUser();

                        MessageBox.Show("SUCSESSFULY DONE");
                    }
                }
                textBox41.Text = "";
                textBox41.Visible = false;
                pictureBox43.Visible = false;
                label43.Visible = false;
                lis.Clear();
            }
        }

        int change = 0;
        private void pictureBox44_Click(object sender, EventArgs e)
        {
            if (change == 0)
            {
                pictureBox44.Image = Image.FromFile(no_sound);
                axWindowsMediaPlayer2.URL = "";
                change = 1;
            }
            else if (change == 1)
            {
                pictureBox44.Image = Image.FromFile(sound);
                axWindowsMediaPlayer2.URL = classik_mus;
                change = 0;
            }
        }

        private void pictureBox45_Click(object sender, EventArgs e)
        {
            pictureBox45.Enabled = false;
            try
            {
                if (checkBox1.Checked == false)
                {
                    foreach (var i in order_list.orders)
                    {
                        if (i.order_id == Int32.Parse(textBox42.Text))
                        {
                            i.restored = true;
                            textBox37.Text = "yes";
                            foreach (var ii in order_list.orders)
                            {
                                if (ii.order_id == Int32.Parse(textBox42.Text))
                                {

                                    foreach (var t in bookt_list.books.ToList())
                                    {
                                        if (ii.book_name == t.Name)
                                        {
                                            t.Quantitiy += ii.quantity;

                                        }

                                    }

                                }
                            }
                            SaveProduct();
                            SaveOrder();
                            redownload(2);
                        }
                    }
                }
                else if (checkBox1.Checked == true)
                {
                    foreach (var i in order_list.orders)
                    {
                        if (i.order_id == Int32.Parse(textBox42.Text))
                        {


                            foreach (var ii in order_list.orders)
                            {
                                if (ii.order_id == Int32.Parse(textBox42.Text))
                                {

                                    foreach (var t in bookt_list.books.ToList())
                                    {
                                        if (i.book_name == t.Name)
                                        {
                                            if (t.Quantitiy >= i.quantity)
                                            {
                                                t.Quantitiy -= ii.quantity;
                                                i.restored = false;
                                                textBox37.Text = "no";
                                            }
                                            else
                                            {
                                                string message = "Not enough books";
                                                string title = "Error";
                                                MessageBoxButtons buttons = MessageBoxButtons.OK;
                                                DialogResult result = MessageBox.Show(message, title, buttons);
                                            }
                                        }

                                    }

                                }
                            }
                            SaveProduct();
                            SaveOrder();
                            redownload(2);
                        }
                    }
                }
            }
            catch { }
        }

        private void pictureBox47_Click(object sender, EventArgs e)
        {
            listView_Borrowed.SelectedItems.Clear();
            textBox39.Text = "";
            textBox35.Text = "";
            textBox36.Text = "";
            textBox30.Text = "";
            textBox37.Text = "";
            textBox33.Text = "";
            textBox34.Text = "";
            textBox42.Text = "";

        }

        private void studentToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void meToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => IsConnectedToInternet());
            thread.Start();
        }
    }
}
