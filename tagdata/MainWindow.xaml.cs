using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace tagdata
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        string[] col_headers = new string[] {
            "糖网","糖网（不确定）","青光眼/视神经萎缩","青光眼/视神经萎缩（不确定）","静脉阻塞","静脉阻塞（不确定）",
            "视网膜脱离","视网膜脱离（不确定）","黄斑病变","黄斑病变（不确定）","视网膜周边变性","视网膜周边变性（不确定）","图像质量差",
            "图像质量差（不确定）","其他（在描述中说明）","正常","正常（不确定）","视盘：水肿","视盘：水肿（不确定）","视盘：出血","视盘：出血（不确定）",
            "视盘：萎缩","视盘：萎缩（不确定）","视盘：图像模糊","视盘：图像模糊（不确定）","视盘：颜色异常","视盘：颜色异常（不确定）","视盘：其他",
            "视盘：其他（不确定）","视盘：正常","视盘：正常（不确定）","黄斑：渗出","黄斑：渗出（不确定）","黄斑：出血","黄斑：出血（不确定）","黄斑：萎缩",
            "黄斑：萎缩（不确定）","黄斑：裂孔","黄斑：裂孔（不确定）","黄斑：网脱","黄斑：网脱（不确定）","黄斑：变性","黄斑：变性（不确定）","黄斑：激光斑",
            "黄斑：激光斑（不确定）","黄斑：其他","黄斑：其他（不确定）","黄斑：正常","黄斑：正常（不确定）","后极：渗出","后极：渗出（不确定）","后极：出血",
            "后极：出血（不确定）","后极：萎缩","后极：萎缩（不确定）","后极：裂孔","后极：裂孔（不确定）","后极：网脱","后极：网脱（不确定）","后极：变性",
            "后极：变性（不确定）","后极：激光斑","后极：激光斑（不确定）","后极：其他","后极：其他（不确定）","后极：正常","后极：正常（不确定）","中周边：渗出",
            "中周边：渗出（不确定）","中周边：出血","中周边：出血（不确定）","中周边：萎缩","中周边：萎缩（不确定）","中周边：裂孔","中周边：裂孔（不确定）","中周边：网脱",
            "中周边：网脱（不确定）","中周边：变性","中周边：变性（不确定）","中周边：激光斑","中周边：激光斑（不确定）","中周边：其他","中周边：其他（不确定）",
            "中周边：正常","中周边：正常（不确定）","周边：渗出","周边：渗出（不确定）","周边：出血","周边：出血（不确定）","周边：萎缩","周边：萎缩（不确定）",
            "周边：裂孔","周边：裂孔（不确定）","周边：网脱","周边：网脱（不确定）","周边：变性","周边：变性（不确定）","周边：激光斑","周边：激光斑（不确定）",
            "周边：其他","周边：其他（不确定）","周边：正常","周边：正常（不确定）","对比_糖网","对比_糖网（不确定）","对比_青光眼/视神经萎缩","对比_青光眼/视神经萎缩（不确定）",
            "对比_静脉阻塞","对比_静脉阻塞（不确定）","对比_视网膜脱离","对比_视网膜脱离（不确定）","对比_黄斑病变","对比_黄斑病变（不确定）","对比_视网膜周边变性",
            "对比_视网膜周边变性（不确定）","对比_图像质量差","对比_图像质量差（不确定）","对比_其他（在描述中说明）","对比_正常","对比_正常（不确定）","对比_视盘：水肿",
            "对比_视盘：水肿（不确定）","对比_视盘：出血","对比_视盘：出血（不确定）","对比_视盘：萎缩","对比_视盘：萎缩（不确定）","对比_视盘：图像模糊","对比_视盘：图像模糊（不确定）",
            "对比_视盘：颜色异常","对比_视盘：颜色异常（不确定）","对比_视盘：其他","对比_视盘：其他（不确定）","对比_视盘：正常","对比_视盘：正常（不确定）","对比_黄斑：渗出",
            "对比_黄斑：渗出（不确定）","对比_黄斑：出血","对比_黄斑：出血（不确定）","对比_黄斑：萎缩","对比_黄斑：萎缩（不确定）","对比_黄斑：裂孔","对比_黄斑：裂孔（不确定）","对比_黄斑：网脱",
            "对比_黄斑：网脱（不确定）","对比_黄斑：变性","对比_黄斑：变性（不确定）","对比_黄斑：激光斑","对比_黄斑：激光斑（不确定）","对比_黄斑：其他","对比_黄斑：其他（不确定）",
            "对比_黄斑：正常","对比_黄斑：正常（不确定）","对比_后极：渗出","对比_后极：渗出（不确定）","对比_后极：出血","对比_后极：出血（不确定）","对比_后极：萎缩","对比_后极：萎缩（不确定）",
            "对比_后极：裂孔","对比_后极：裂孔（不确定）","对比_后极：网脱","对比_后极：网脱（不确定）","对比_后极：变性","对比_后极：变性（不确定）","对比_后极：激光斑","对比_后极：激光斑（不确定）",
            "对比_后极：其他","对比_后极：其他（不确定）","对比_后极：正常","对比_后极：正常（不确定）","对比_中周边：渗出","对比_中周边：渗出（不确定）","对比_中周边：出血","对比_中周边：出血（不确定）",
            "对比_中周边：萎缩","对比_中周边：萎缩（不确定）","对比_中周边：裂孔","对比_中周边：裂孔（不确定）","对比_中周边：网脱","对比_中周边：网脱（不确定）","对比_中周边：变性","对比_中周边：变性（不确定）",
            "对比_中周边：激光斑","对比_中周边：激光斑（不确定）","对比_中周边：其他","对比_中周边：其他（不确定）","对比_中周边：正常","对比_中周边：正常（不确定）","对比_周边：渗出",
            "对比_周边：渗出（不确定）","对比_周边：出血","对比_周边：出血（不确定）","对比_周边：萎缩","对比_周边：萎缩（不确定）","对比_周边：裂孔","对比_周边：裂孔（不确定）","对比_周边：网脱",
            "对比_周边：网脱（不确定）","对比_周边：变性","对比_周边：变性（不确定）","对比_周边：激光斑","对比_周边：激光斑（不确定）","对比_周边：其他","对比_周边：其他（不确定）","对比_周边：正常",
            "对比_周边：正常（不确定）",
        };
        // 记录，存放进listbox中
        public ObservableCollection<string> loglist = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// T/F转换成阳性或在阴性
        /// </summary>
        public static string TFtoLook(string check)
        {
            if (check == "True")
                return "阳性";
            else if (check == "False")
                return  "阴性";
            else
                return "无数据";
        }

        /// <summary>
        /// 遍历获取文件夹下的所有文件
        /// </summary>
        /// <param name="filePathByForeach"></param>
        /// <param name="result"></param>
        public static void ForeachFile(string filePathByForeach, ref List<string> result)
        {
            DirectoryInfo theFolder = new DirectoryInfo(filePathByForeach);
            DirectoryInfo[] dirInfo = theFolder.GetDirectories();//获取所在目录的文件夹
            FileInfo[] file = theFolder.GetFiles();//获取所在目录的文件

            foreach (FileInfo fileItem in file) //遍历文件
            {
                result.Add(fileItem.FullName);
            }
            //遍历文件夹
            foreach (DirectoryInfo NextFolder in dirInfo)
            {
                ForeachFile(NextFolder.FullName, ref result);
            }
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="col"></param>
        private static DataTable CreateDataTable(List<string> col)
        {
            DataTable dt = new DataTable("table");
            foreach (var item in col)
            {
                dt.Columns.Add(item, typeof(string));
            }
            return dt;
        }
        /// <summary>
        /// 添加一行数据
        /// </summary>
        private static void AddData(List<string> data, DataTable dt)
        {
            DataRow dr = dt.NewRow();
            int tmp = 0;
            foreach (var item in data)
            {
                dr[tmp] = item;
                tmp++;
            }
            dt.Rows.Add(dr);
        }

        /// <summary>
        /// 生成csv文件
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="pathFile"></param>
        /// <returns></returns>
        public static bool datatableToCSV(DataTable dt, string pathFile)
        {
            string strLine = "";
            StreamWriter sw;
            try
            {
                sw = new StreamWriter(pathFile, false, System.Text.Encoding.GetEncoding(-0));
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i > 0)
                        strLine += ",";
                    strLine += dt.Columns[i].ColumnName;
                }
                strLine.Remove(strLine.Length - 1);
                sw.WriteLine(strLine);
                strLine = "";
                //表的内容
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    strLine = "";
                    int colCount = dt.Columns.Count;
                    for (int k = 0; k < colCount; k++)
                    {
                        if (k > 0 && k < colCount)
                            strLine += ",";
                        if (dt.Rows[j][k] == null)
                            strLine += "";
                        else
                        {
                            string cell = dt.Rows[j][k].ToString().Trim();
                            //防止里面含有特殊符号
                            cell = cell.Replace("\"", "\"\"");
                            cell = "\"" + cell + "\"";
                            strLine += cell;
                        }
                    }
                    sw.WriteLine(strLine);
                }
                sw.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 读取JSON文件
        /// </summary>
        /// <param name="key">JSON文件中的key值</param>
        /// <returns>JSON文件中的value值</returns>
        public static string Readjson(string jsonpath, string key)
        {
            try
            {
                using (FileStream fs = new FileStream(jsonpath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader file = new StreamReader(fs, System.Text.Encoding.Default))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            JObject o = (JObject)JToken.ReadFrom(reader);
                            var value = o["flags"][key].ToString();
                            return value;
                        }
                    }
                }
            }
            catch
            {
                return "";
            }
        }

        public void AddLog(string log)
        {
            Application.Current.Dispatcher.Invoke(() => {
                loglist.Add(log);
                // 显示进listbox
                log_listbox.ItemsSource = null;
                log_listbox.ItemsSource = loglist;
            });
        }

        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            HamburgerMenuControl.Content = e.InvokedItem;
        }


        #region 文件查找
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                targetdir.Text = openFileDialog.SelectedPath;
            }
        }

        /// <summary>
        /// 设置查找文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                finddir.Text = openFileDialog.SelectedPath;
            }
        }

        /// <summary>
        /// 设置保存路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                copydir.Text = openFileDialog.SelectedPath;
            }
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AddLog("--------文件查找--------");
            mask.Visibility = Visibility.Visible;
            string targetname_text = targetname.Text;
            string findname_text = findname.Text;
            string targetdir_text = targetdir.Text;
            string finddir_text = finddir.Text;
            string copydir_text = copydir.Text;
            Task.Factory.StartNew(() =>
            {
                try
                {
                    // 判断是目录还是文件
                    if (Directory.Exists(targetdir_text))
                    {
                        // 目录
                        // 读取所有查找的文件
                        List<string> target_lib = new List<string>();
                        ForeachFile(targetdir_text, ref target_lib);
                        // 读取所有被查找文件
                        List<string> find_img = new List<string>();
                        ForeachFile(finddir_text, ref find_img);

                        // 获取需要查找的类型，所有文件
                        var resultFiles = target_lib.Where(t => t.Contains(targetname_text) == true).ToList();
                        foreach (var item in resultFiles)
                        {
                            // 获取需要查找的文件名
                            string find = Path.GetFileName(item).Replace(targetname_text, findname_text);
                            // 查找对比标签文件全路径
                            string full = find_img.Find(path => Path.GetFileName(path) == find);
                            if (full is string)
                            {
                                File.Copy(full, Path.Combine(copydir_text, find), true);
                                AddLog($"查找成功 ---> 文件名：{find}");
                            }
                            else
                            {
                                AddLog($"查找失败 ---> 文件名：{find}");
                            }
                        }
                    }
                    else
                    {
                        string s = File.ReadAllText(targetdir_text);
                        string[] dataInfo = s.Replace("\r\n", "?").Split('?');
                        // 读取所有被查找文件
                        List<string> find_img = new List<string>();
                        ForeachFile(finddir_text, ref find_img);
                        foreach (var i in dataInfo)
                        {
                            //获得文件扩展名
                            string fileNameEx = Path.GetExtension(i);
                            // 获取需要查找的文件名
                            string find = i.Replace(fileNameEx, findname_text);
                            // 查找对比标签文件全路径
                            string full = find_img.Find(path => Path.GetFileName(path) == find);
                            if (full is string)
                            {
                                File.Copy(full, Path.Combine(copydir_text, find), true);
                                AddLog($"查找成功 ---> 文件名：{find}");
                            }
                            else
                            {
                                AddLog($"查找失败 ---> 文件名：{find}");
                            }
                        }
                    }
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        mask.Visibility = Visibility.Hidden;
                        this.ShowMessageAsync("查找完成。", this.Title, MessageDialogStyle.Affirmative);
                    });
                }
                catch
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        mask.Visibility = Visibility.Hidden;
                        this.ShowMessageAsync("出错了，检查一下吧？", this.Title, MessageDialogStyle.Affirmative);
                    });
                }
            });
        }
        #endregion

        #region 病症查找
        /// <summary>
        /// 选择标记文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void select_sy_lbl(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sreach_sy_lbl.Text = openFileDialog.SelectedPath;
            }
        }

        /// <summary>
        /// 选择图片文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void select_sy_img(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sreach_sy_img.Text = openFileDialog.SelectedPath;
            }
        }

        /// <summary>
        /// 设置输入路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void select_sy_import(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sreach_sy_import.Text = openFileDialog.SelectedPath;
            }
        }

        /// <summary>
        /// 输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sreach(object sender, RoutedEventArgs e)
        {
            AddLog("--------病症查找--------");
            string sreach_sy_text = sreach_sy.Text;
            string sreach_sex_text = sreach_sex.Text;
            if (sreach_sex_text == "阳性")
                sreach_sex_text = "True";
            if (sreach_sex_text == "阴性")
                sreach_sex_text = "False";
            string sreach_sy_lbl_text = sreach_sy_lbl.Text;
            string sreach_sy_img_text = sreach_sy_img.Text;
            string sreach_sy_import_text = sreach_sy_import.Text;
            mask.Visibility = Visibility.Visible;
            Task.Factory.StartNew(() =>
            {
                try
                {
                    // 创建表
                    List<string> dataTableHeader = new List<string>();
                    dataTableHeader.Add("图片名");
                    dataTableHeader.Add(sreach_sy_text);
                    dataTableHeader.Add($"{sreach_sy_text}（不确定）");
                    DataTable dataTable = CreateDataTable(dataTableHeader);
                    // 读取所有标记文件
                    List<string> res_lib = new List<string>();
                    ForeachFile(sreach_sy_lbl_text, ref res_lib);
                    // 读取所有图片文件
                    List<string> res_img = new List<string>();
                    ForeachFile(sreach_sy_img_text, ref res_img);
                    // 获取病症为true的标签
                    foreach (string item in res_lib)
                    {
                        var result = Readjson(item, sreach_sy_text);
                        if(result == "")
                        {
                            AddLog($"标记文件{Path.GetFileName(item)}没有此病症，请检查。");
                            continue;
                        }
                        // 添加表数据
                        List<string> colData = new List<string>();
                        colData.Add(Path.GetFileName(item).Replace(".lbl", ".jpg"));
                        colData.Add(TFtoLook(Readjson(item, sreach_sy_text)));
                        colData.Add(TFtoLook(Readjson(item, $"{sreach_sy_text}（不确定）")));
                        AddData(colData, dataTable);
                        // 病症选择上的，复制出图片和标记文件
                        if (result == sreach_sex_text)
                        {
                            // 获取文件名,是标签文件名
                            string lblname = Path.GetFileName(item);
                            // 复制标记文件
                            if (File.Exists(item))
                            {
                                File.Copy(item, Path.Combine(sreach_sy_import_text, lblname), true);
                            }
                            // 复制图片
                            string imgname = lblname.Replace(".lbl", ".jpg");
                            // 查找图片文件全路径
                            string img_full = res_img.Find(imgPath => Path.GetFileName(imgPath) == imgname);
                            if (img_full is string)
                            {
                                File.Copy(img_full, Path.Combine(sreach_sy_import_text, imgname), true);
                                AddLog($"查找成功 ---> 图片名：{imgname}");
                            }
                            else
                            {
                                AddLog($"查找失败 ---> 图片名：{imgname}");
                            }
                        }
                    }
                    // 生成csv文件
                    if (datatableToCSV(dataTable, Path.Combine(sreach_sy_import_text, "病症所有数据.csv")))
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            mask.Visibility = Visibility.Hidden;
                            this.ShowMessageAsync("查找完成。", this.Title, MessageDialogStyle.Affirmative);
                        });
                    }
                }
                catch
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        mask.Visibility = Visibility.Hidden;
                        this.ShowMessageAsync("出错了，检查一下吧？", this.Title, MessageDialogStyle.Affirmative);
                    });
                }
            });
        }
        #endregion

        #region 分类对比
        /// <summary>
        /// 选择标签
        /// </summary>
        private void cls_lbl(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cls_lbl_name.Text = openFileDialog.SelectedPath;
            }
        }

        /// <summary>
        /// 对比标签
        /// </summary>
        private void c_cls_lbl(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                c_cls_lbl_name.Text = openFileDialog.SelectedPath;
            }
        }

        /// <summary>
        /// 选择图片
        /// </summary>
        private void cls_img(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cls_img_name.Text = openFileDialog.SelectedPath;
            }
        }

        /// <summary>
        /// 现在输出位置
        /// </summary>
        private void cls_import(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cls_import_name.Text = openFileDialog.SelectedPath;
            }
        }

        /// <summary>
        /// 对比查找
        /// </summary>
        private void cls_search(object sender, RoutedEventArgs e)
        {
            mask.Visibility = Visibility.Visible;
            loglist.Add("--------分类查找--------");
            string cls_sy_text = cls_sy.Text;
            string cls_lbl_name_text = cls_lbl_name.Text;
            string c_cls_lbl_name_text = c_cls_lbl_name.Text;
            string cls_img_name_text = cls_img_name.Text;
            string cls_import_name_text = cls_import_name.Text;
            Task.Factory.StartNew(() =>
            {
                try
                {
                    // 获取标记和对比标记所有文件
                    // 读取所有标记文件
                    List<string> res_lib = new List<string>();
                    List<string> res_lib_c = new List<string>();
                    List<string> res_img = new List<string>();
                    ForeachFile(cls_lbl_name_text, ref res_lib);
                    ForeachFile(c_cls_lbl_name_text, ref res_lib_c);
                    ForeachFile(cls_img_name_text, ref res_img);
                    // 创建表
                    List<string> dataTableHeader = new List<string>();
                    // 对比表头
                    dataTableHeader.Add("图片名");
                    foreach (var item in col_headers)
                    {
                        dataTableHeader.Add(item);
                    }
                    DataTable dataTable = CreateDataTable(dataTableHeader);
                    foreach (string item in res_lib)
                    {
                        // 数据填充进表格
                        // 添加表数据
                        List<string> colData = new List<string>();
                        // 存储对比表数据，在一起加进去
                        List<string> c_colData = new List<string>();
                        colData.Add(Path.GetFileName(item).Replace(".lbl", ".jpg"));
                        for (int i = 0; i < col_headers.Length; i++)
                        {
                            if (col_headers[i].Contains("对比_") == false)
                            {
                                var result = Readjson(item, col_headers[i]);
                                // 标记文件有错，就丢弃
                                if(result == "")
                                {
                                    c_colData.Add("");
                                    AddLog($"标记文件{Path.GetFileName(item)}没有{col_headers[i]}病症，请检查。");
                                }
                                else
                                    colData.Add(TFtoLook(result));
                            }
                            else
                            {
                                // 查找对比标签文件全路径
                                string c_full = res_lib_c.Find(cPath => Path.GetFileName(cPath) == Path.GetFileName(item));
                                if (c_full is string)
                                {
                                    var c_result = Readjson(c_full, col_headers[i].Replace("对比_", ""));
                                    // 被对比标记文件有错，写空值
                                    if (c_result == "")
                                    {
                                        c_colData.Add("");
                                        AddLog($"被对比的标记文件{Path.GetFileName(c_full)}没有{col_headers[i]}病症，请检查。");
                                    }
                                    else
                                        c_colData.Add(TFtoLook(c_result));
                                }
                            }
                        }
                        colData = colData.Concat(c_colData).ToList<string>();
                        AddData(colData, dataTable);

                        // 查找需要对比筛选的数据,复制出相同阳性的相同阴性的
                        int index = Array.IndexOf(col_headers, cls_sy_text) + 1;
                        int c_index = Array.IndexOf(col_headers, $"对比_{cls_sy_text}") + 1;
                        if (colData[index] == "阳性" && colData[c_index] == "阳性")
                        {
                            string path = Path.Combine(cls_import_name_text, $"{cls_sy_text}阳性一致数据");
                            if (Directory.Exists(path) == false)
                            {
                                Directory.CreateDirectory(path);
                            }
                            // 复制图片
                            string imgname = colData[0];
                            // 查找图片文件全路径
                            string img_full = res_img.Find(imgPath => Path.GetFileName(imgPath) == imgname);
                            if (img_full is string)
                            {
                                File.Copy(img_full, Path.Combine(path, imgname), true);
                                AddLog($"查找阳性数据成功 ---> 图片名：{imgname}");
                            }
                            else
                            {
                                AddLog($"查找阳性数据失败 ---> 图片名：{imgname}");
                            }
                        }
                        else if (colData[index] == "阴性" && colData[c_index] == "阴性")
                        {
                            string path = Path.Combine(cls_import_name_text, $"{cls_sy_text}阴性一致数据");
                            if (Directory.Exists(path) == false)
                            {
                                Directory.CreateDirectory(path);
                            }
                            // 复制图片
                            string imgname = colData[0];
                            // 查找图片文件全路径
                            string img_full = res_img.Find(imgPath => Path.GetFileName(imgPath) == imgname);
                            if (img_full is string)
                            {
                                File.Copy(img_full, Path.Combine(path, imgname), true);
                                AddLog($"查找阴性数据成功 ---> 图片名：{imgname}");
                            }
                            else
                            {
                                AddLog($"查找阴性数据失败 ---> 图片名：{imgname}");
                            }
                        }
                        else
                        {
                            AddLog($"图片名：{colData[0]} ---> 为不一致图片");
                        }
                    }
                    // 生成csv文件
                    if (datatableToCSV(dataTable, Path.Combine(cls_import_name_text, "对比数据.csv")))
                    {
                        Application.Current.Dispatcher.Invoke(() => {
                            mask.Visibility = Visibility.Hidden;
                            this.ShowMessageAsync("查找成功。", this.Title, MessageDialogStyle.Affirmative);
                        });
                    }
                }
                catch
                {
                    Application.Current.Dispatcher.Invoke(() => {
                        mask.Visibility = Visibility.Hidden;
                        this.ShowMessageAsync("出错了，检查一下吧？", this.Title, MessageDialogStyle.Affirmative);
                    });
                }
            });
        }
        #endregion

        #region 查找记录
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_record_import(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                record_import.Text = openFileDialog.SelectedPath;
            }
        }

        /// <summary>
        /// 生成txt
        /// </summary>
        private async void record_txt(object sender, RoutedEventArgs e)
        {
            try
            {
                if (loglist.Count > 0)
                {
                    using (FileStream fs = new FileStream(Path.Combine(record_import.Text, "操作记录.txt"), FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            sw.Flush();
                            sw.BaseStream.Seek(0, SeekOrigin.Begin);
                            for (int i = 0; i < loglist.Count; i++) sw.WriteLine(loglist[i]);
                        }
                    }
                    await this.ShowMessageAsync("操作记录生成成功。", this.Title, MessageDialogStyle.Affirmative);
                }
                else
                    await this.ShowMessageAsync("出错了，检查一下吧？", this.Title, MessageDialogStyle.Affirmative);
            }
            catch
            {
                await this.ShowMessageAsync("出错了，检查一下吧？", this.Title, MessageDialogStyle.Affirmative);
            }
        }

        /// <summary>
        /// 清空
        /// </summary>
        private void record_clear(object sender, RoutedEventArgs e)
        {
            loglist.Clear();
            log_listbox.ItemsSource = null;
        }
        #endregion
    }
}
