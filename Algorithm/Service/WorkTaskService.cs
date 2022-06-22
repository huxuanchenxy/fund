using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Caching.Distributed;
using MSS.API.Common;
using MSS.API.Common.Utility;
using MSS.Platform.Workflow.WebApi.Data;
using MSS.Platform.Workflow.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.Platform.Workflow.WebApi.Service
{
    public class WorkTaskService : IWorkTaskService
    {
        private readonly IWorkTaskRepo<TaskViewModel> _repo;
        private readonly IAuthHelper _authhelper;
        private readonly IDistributedCache _cache;
        public WorkTaskService(IWorkTaskRepo<TaskViewModel> repo, IAuthHelper authhelper, IDistributedCache cache)
        {
            _repo = repo;
            _authhelper = authhelper;
            _cache = cache;
        }


        public async Task<ApiResult> Algorithm(string s, string s1)
        {
            ApiResult ret = new ApiResult();
            try
            {
                string[] string1 = s.Split(',');
                int[] parm = new int[string1.Length];
                for (int i = 0; i < string1.Length; i++)
                {
                    parm[i] = int.Parse(string1[i]);
                }

                string[] string2 = s1.Split(',');
                int[] parm1 = new int[string2.Length];
                for (int i = 0; i < string2.Length; i++)
                {
                    parm1[i] = int.Parse(string2[i]);
                }

                //TreeNode root = new TreeNode() { val = 3, right = new TreeNode() { val = 20, left = new TreeNode() { val = 15 }, right = new TreeNode() { val = 7 } } };
                //string[] parm = new string[] {"test.email+alex@leetcode.com", "test.email@leetcode.com" };
                ret.data = FindMedianSortedArrays(parm, parm1);
                ret.code = Code.Success;
                //ret.data = data;
            }
            catch (Exception ex)
            {
                ret.code = Code.Failure;
                ret.msg = ex.Message;
            }

            return ret;
        }


        //1. 两数之和
        public int[] TwoSum(int[] nums, int target)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (dic.ContainsKey(target - nums[i]))
                {
                    return new int[] { dic[target - nums[i]], i };
                }
                if (!dic.ContainsKey(nums[i]))
                {
                    dic.Add(nums[i], i);
                }

            }
            return new int[0];
        }


        //4. 寻找两个正序数组的中位数
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            int m = nums1.Length;
            int n = nums2.Length;


            if (m == 0)//这时下面的 nums1[i] < nums2[j] 会超边界报错，所以要特殊判断
            {
                if ((n & 1) != 0)//奇数
                {
                    return (double)nums2[n / 2];
                }
                else
                {
                    return (double)(nums2[n / 2] + nums2[n / 2 - 1]) / 2.0;
                }
            }
            if (n == 0)
            {
                if ((m & 1) != 0)
                {
                    return (double)nums1[m / 2];
                }
                else
                {
                    return (double)(nums1[m / 2] + nums1[m / 2 - 1]) / 2.0;
                }
            }


            int p = 0;
            int[] arr = new int[m + n];
            int i = 0;
            int j = 0;



            while (p < (m + n))
            {
                if (nums1[i] < nums2[j])
                {
                    arr[p] = nums1[i];
                    i++;
                }
                else
                {
                    arr[p] = nums2[j];
                    j++;//这一步，当前的nums2有可能超边界了，会导致 上面的 nums1[i] < nums2[j] 报错
                }
                p++;

                if (j == n)//超边界了,则把剩余的m全部放入arr里
                {
                    while (p < (m + n))
                    {
                        arr[p] = nums1[i++];
                        p++;
                    }
                    break;
                }
                if (i == m)//i 超边界了，则把剩余的n全部放到arr里
                {
                    while (p < (m + n))
                    {
                        arr[p] = nums2[j++];
                        p++;
                    }
                    break;
                }
            }

            int len = m + n;
            if ((len & 1) != 0)//奇数
            {
                return (double)arr[len / 2];
            }
            else
            {
                return (double)((arr[len / 2] * 1.0 + arr[len / 2 - 1] * 1.0) / 2.0);
            }
        }

        public int LastStoneWeight(int[] stones)
        {
            List<int> list = new List<int>();
            foreach (var s in stones)
            {
                list.Add(s);
            }
            list.Sort();
            int ret = 0;
            while (list.Count != 0)
            {
                if (list.Count == 1)
                {
                    ret = list[0];
                    break;
                }
                int big = list[list.Count - 1];
                list.RemoveAt(0);
                int small = list[list.Count - 1];
                list.RemoveAt(0);
                if (big != small)
                {
                    list.Add(big - small);
                    list.Sort();
                }

            }
            return ret;
        }

        public int NumUniqueEmails(string[] emails)
        {
            HashSet<string> hash = new HashSet<string>();
            foreach (var e in emails)
            {
                string[] str = e.Split('@');
                string local = str[0].Replace(".", "");
                int indexfirstplus = local.IndexOf('+');
                if (indexfirstplus >= 0)
                {
                    local = local.Substring(0, indexfirstplus - 1);
                }
                string cur = local + str[1];
                if (!hash.Contains(cur))
                {
                    hash.Add(cur);
                }

            }
            return hash.Count;
        }

        public int RobotSim(int[] commands, int[][] obstacles)
        {
            int x = 0, y = 0;//当前坐标位置
            int ret = 0;
            int direction = 0;//0北1东2南3西 是dx dy的下标
                              // int[] dx = new int[4]{0,1,0,-1};
                              // int[] dy = new int[4]{1,0,-1,0}; 
            int[] dx = new int[] { 0, 1, 0, -1 };
            int[] dy = new int[] { 1, 0, -1, 0 };


            HashSet<point> hash = new HashSet<point>();//先保存所有障碍坐标
            foreach (var o in obstacles)
            {
                hash.Add(new point(o[0], o[1]));
            }
            for (int i = 0; i < commands.Length; i++)
            {
                if (commands[i] == -2)
                {
                    direction = (direction + 3) % 4;//对应到dx或者dy的四个下标
                }
                else if (commands[i] == -1)
                {
                    direction = (direction + 1) % 4;
                }
                else
                {
                    //开始一步一步走，走到障碍物就停止
                    for (int j = 1; j < commands[i]; j++)
                    {
                        // var nextX = x + dx[direction];
                        // var nextY = y + dy[direction];
                        // var curP = new point(nextX,nextY);
                        // if(hash.Contains(curP))
                        // {
                        //     break;
                        // }else{
                        //     x = nextX;
                        //     y = nextY;
                        //     ret = Math.Max(ret,x*x+ y*y);
                        // }
                        if (hash.Contains(new point(x + dx[direction], y + dy[direction])))
                        {
                            break;
                        }
                        else
                        {
                            x += dx[direction];
                            y += dy[direction];
                        }

                    }
                }
                ret = Math.Max(ret, (int)Math.Pow(x, 2) + (int)Math.Pow(y, 2));

            }
            return ret;
        }

        struct point
        {
            public int X;
            public int Y;
            public point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }

        public string MostCommonWord(string paragraph, string[] banned)
        {
            string p1 = "";
            foreach (var p in paragraph)
            {
                if (char.IsLetter(p))
                {
                    p1 += p;
                }
                else
                {
                    p1 += ' ';
                }
            }
            string[] arr = p1.ToLower().Split(' ');
            Dictionary<string, int> dic = new Dictionary<string, int>();
            foreach (var a in arr)
            {
                if (Array.IndexOf(banned, a) < 0 && !string.IsNullOrEmpty(a))
                {
                    if (!dic.ContainsKey(a))
                    {
                        dic.Add(a, 1);
                    }
                    else
                    {
                        dic[a]++;
                    }
                }
            }
            string ret = "";
            Dictionary<string, int> dic2 = dic.OrderByDescending(o => o.Value).ToDictionary(o => o.Key, p => p.Value);
            ret = dic2.First().Key;

            return ret;
        }


        /// <summary>
        /// 111. 二叉树的最小深度
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public int minDepth(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            // 当前节点的左右子树都为空，则该节点为叶子节点，返回1
            if (root.left == null && root.right == null)
            {
                return 1;
            }

            // 计算左子树的最小深度
            int leftDepth = minDepth(root.left);
            // 计算右子树的最小深度
            int rightDepth = minDepth(root.right);

            // 当前节点的左右子树都不为空
            // 则返回左右子树中深度较小的那个+1
            if (root.left != null && root.right != null)
            {
                return Math.Min(leftDepth, rightDepth) + 1;
            }

            // 当前节点的左子树或右子树为空
            // 则返回不为空的那个子树的深度+1
            if (root.left != null)
            {
                return leftDepth + 1;
            }
            return rightDepth + 1;
        }

        /// <summary>
        /// 724. 寻找数组的中心下标
        /// https://leetcode-cn.com/problems/find-pivot-index/solution/zi-ji-cuo-wu-de-whileleftrightde-si-lu-h-08o9/
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int PivotIndex(int[] nums)
        {
            int sum = 0;
            foreach (var n in nums)
            {
                sum += n;
            }
            int length = nums.Length;
            if (length == 0 || length == 2) return -1;
            if (length == 1) return 0;

            int sumLeft = 0;

            for (int i = 0; i < length; i++)
            {
                if (sumLeft == sum - nums[i]) return i;
                sumLeft += nums[i];
                sum -= nums[i];
            }

            return -1;
        }

        public int FindShortestSubArray(int[] nums)
        {
            Dictionary<int, int> left = new Dictionary<int, int>();//第一次出现的位置
            Dictionary<int, int> right = new Dictionary<int, int>();//最后一次出现的位置
            Dictionary<int, int> count = new Dictionary<int, int>();//出现的次数
            int maxDegree = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                var cur = nums[i];
                if (!count.ContainsKey(cur))
                {
                    count.Add(cur, 1);
                    maxDegree = 1;
                }
                else
                {
                    count[cur]++;
                    maxDegree = Math.Max(maxDegree, count[cur]);
                }

                if (!left.ContainsKey(cur))//没有就记录第一次出现,并赋予第一次right
                {
                    left.Add(cur, i);
                    right.Add(cur, i);
                }
                else
                {
                    right[cur] = i;//有的话则更新right
                }
            }

            int ret = nums.Length;
            foreach (KeyValuePair<int, int> kv in count)
            {
                var curDegreee = kv.Value;
                var curKey = kv.Key;
                if (curDegreee == maxDegree)
                {
                    int tmp = right[curKey] - left[curKey] + 1;
                    ret = Math.Min(ret, tmp);
                }
            }
            return ret;

        }

        public string[] FindRestaurant(string[] list1, string[] list2)
        {
            List<MyDic> mylist = new List<MyDic>();
            for (int i = 0; i < list1.Length; i++)
            {
                for (int j = 0; j < list2.Length; j++)
                {
                    if (list1[i] == list2[j])
                    {
                        mylist.Add(new MyDic() { MyKey = i + j, MyString = list1[i] });
                    }
                }
            }

            int curMin = mylist[0].MyKey;
            List<string> ret = new List<string>();
            ret.Add(mylist[0].MyString);
            for (int i = 1; i < mylist.Count; i++)
            {
                if (mylist[i].MyKey < curMin)
                {
                    ret.Clear();
                }
                else if (mylist[i].MyKey == curMin)
                {
                    ret.Add(mylist[i].MyString);
                }
            }
            return ret.ToArray();
        }

        /// <summary>
        /// 496. 下一个更大元素 I   https://leetcode-cn.com/problems/next-greater-element-i/
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="nums2"></param>
        /// <returns></returns>
        public int[] NextGreaterElement(int[] nums1, int[] nums2)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            Stack<int> st = new Stack<int>();
            for (int i = 0; i < nums2.Length; i++)
            {
                while (st.Count != 0 && nums2[i] > st.Peek())
                {
                    dic.Add(st.Pop(), nums2[i]);
                }
                st.Push(nums2[i]);
            }
            while (st.Count != 0)
            {
                dic.Add(st.Pop(), -1);
            }
            for (int i = 0; i < nums1.Length; i++)
            {
                int cur = dic[nums1[i]];
                nums1[i] = cur;
            }
            return nums1;
        }


        public bool RepeatedSubstringPattern(string s)
        {
            string ss = s + s;
            return ss.IndexOf(s, 1) != s.Length;
        }

        /// <summary>
        /// 349. 两个数组的交集
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="nums2"></param>
        /// <returns></returns>
        private int[] Intersection(int[] nums1, int[] nums2)
        {
            HashSet<int> hash = new HashSet<int>();
            Array.Sort<int>(nums1);
            Array.Sort<int>(nums2);
            int i = 0;
            int j = 0;
            while (i < nums1.Length && j < nums2.Length)
            {
                if (nums1[i] == nums2[j])
                {
                    if (!hash.Contains(nums1[i]))
                    {
                        hash.Add(nums1[i]);
                    }
                    i++;
                    j++;
                }
                else if (nums1[i] < nums2[j])
                {
                    i++;
                }
                else if (nums1[i] > nums2[j])
                {
                    j++;
                }
            }
            int[] ret = new int[hash.Count];
            int index = 0;
            foreach (var tmp in hash)
            {
                ret[index++] = tmp;
            }
            return ret;
        }

        private bool WordPattern(string pattern, string s)
        {
            string[] sArr = s.Split(' ');
            StringBuilder s1 = new StringBuilder();
            StringBuilder s2 = new StringBuilder();
            for (int i = 0; i < pattern.Length; i++)
            {
                s1.Append(pattern.IndexOf(pattern[i]));
                s2.Append(sArr.IndexOf(sArr[i]));
            }
            return s1.ToString() == s2.ToString();
        }

        /// <summary>
        /// 234. 回文链表
        /// https://leetcode-cn.com/problems/palindrome-linked-list/solution/di-gui-zhan-deng-3chong-jie-jue-fang-shi-zui-hao-d/
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        private bool IsPalindrome(ListNode head)
        {
            ListNode tmp = head;
            Stack<int> st = new Stack<int>();
            while (tmp != null)
            {
                st.Push(tmp.val);
                tmp = tmp.next;
            }
            while (head != null)
            {
                if (head.val != st.Pop())
                {
                    return false;
                }
                head = head.next;
            }
            return true;
        }
        private bool ContainsNearbyDuplicate(int[] nums, int k)
        {
            Queue<int> q = new Queue<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (q.Contains(nums[i]))
                {
                    return true;
                }
                q.Enqueue(nums[i]);
                if (q.Count > k)
                {
                    q.Dequeue();
                }
            }
            return false;
        }

        /// <summary>
        /// 205. 同构字符串 https://leetcode-cn.com/problems/isomorphic-strings/
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private bool IsIsomorphic(string s, string t)
        {
            StringBuilder s1 = new StringBuilder();
            StringBuilder t1 = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                s1.Append(s.IndexOf(s[i]));
                t1.Append(t.IndexOf(t[i]));
            }
            return s1.ToString() == t1.ToString();
        }

        private int LengthOfLastWord(string s)
        {
            int ret = 0;
            int end = -1;
            int start = -1;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] == ' ')
                {
                    continue;
                }
                else
                {
                    if (end == -1)
                    {
                        end = i;
                        start = end;
                        ret = end - start + 1;
                        if (start - 1 == -1)
                        {
                            return 1;
                        }
                    }
                    else
                    {

                        if (s[start - 1] == ' ')
                        {
                            ret = end - start + 1;
                            break;
                        }
                        else
                        {
                            start--;
                        }
                        ret = end - start + 1;
                    }
                }

            }
            return ret;
        }

        /// <summary>
        /// 二进制求和
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private string AddBinary(string a, string b)
        {
            string ret = string.Empty;
            if (a.Length > b.Length)
            {
                b = b.PadLeft(a.Length, '0');
            }
            if (a.Length < b.Length)
            {
                a = a.PadLeft(b.Length, '0');
            }
            int carry = 0;
            for (int i = a.Length - 1; i >= 0; i--)
            {
                int curold = (int.Parse(a[i].ToString()) + int.Parse(b[i].ToString()));//当前无脑加起来的和
                int curnew = (curold + carry);//当前加上上一位的进位新的数字
                carry = curnew >= 2 ? 1 : 0;//下一位是否进位
                curnew = curnew % 2;//计算出当前应该是几
                ret = curnew + ret;//拼接结果
            }
            ret = carry == 1 ? 1 + ret : ret;

            return ret;
        }

        /// <summary>
        /// https://leetcode-cn.com/problems/maximum-subarray/solution/hua-jie-suan-fa-53-zui-da-zi-xu-he-by-guanpengchn/
        /// 最大子序和
        /// 神奇的正数增益
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        private int MaxSubArray(int[] nums)
        {
            int ret = nums[0];
            int sum = 0;
            foreach (var num in nums)
            {
                if (sum <= 0)
                {
                    sum = num;
                }
                else
                {
                    sum += num;
                }
                ret = Math.Max(ret, sum);
            }
            return ret;
        }

        private int MaxDepth(TreeNode nodes)
        {
            if (nodes == null)
            {
                return 0;
            }
            int deep = 0;
            Queue<TreeNode> que = new Queue<TreeNode>();
            que.Enqueue(nodes);
            while (que.Count != 0)
            {
                deep++;
                int size = que.Count;
                if (size > 0)
                {
                    for (int i = 0; i < size; i++)
                    {
                        TreeNode cur = que.Dequeue();
                        if (cur.left != null)
                        {
                            que.Enqueue(cur.left);
                        }
                        if (cur.right != null)
                        {
                            que.Enqueue(cur.right);
                        }
                    }
                }
            }
            return deep;
        }

        /// <summary>
        /// 前序打印二叉树
        /// </summary>
        /// <param name="tree"></param>
        private string PreOrder(TreeNode tree)
        {
            string ret = string.Empty;
            Stack<TreeNode> st = new Stack<TreeNode>();
            st.Push(tree);
            while (st.Count != 0)
            {
                TreeNode t = st.Pop();
                ret += t.val + ",";
                if (t.right != null)
                {
                    st.Push(t.right);
                }
                if (t.left != null)
                {
                    st.Push(t.left);
                }
            }
            return ret;
        }

        private void FirstPrint(TreeNode node)
        {
            if (node == null)
            {
                return;
            }
            Console.Write(node.val);
            FirstPrint(node.left);
            FirstPrint(node.right);
        }

        private TreeNode CreateTree()
        {
            TreeNode nodeA = new TreeNode();
            nodeA.val = 1;
            TreeNode nodeB = new TreeNode();
            nodeB.val = 2;
            TreeNode nodeC = new TreeNode();
            nodeC.val = 3;
            TreeNode nodeD = new TreeNode();
            nodeD.val = 4;
            TreeNode nodeE = new TreeNode();
            nodeE.val = 5;
            TreeNode nodeF = new TreeNode();
            nodeF.val = 6;

            nodeB.left = nodeD;
            nodeB.right = nodeE;
            nodeC.left = nodeF;
            nodeA.left = nodeB;
            nodeA.right = nodeC;
            return nodeA;

        }

        /// <summary>
        /// 加一
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        public int[] PlusOne(int[] digits)
        {
            int length = digits.Length;
            for (int i = length - 1; i >= 0; i--)
            {
                if (digits[i] != 9)
                {
                    digits[i]++;//当前的位置不是9的加1即可
                    return digits;
                }
                else
                {
                    digits[i] = 0;//当前是9的肯定变成0，进位不用管
                }
            }
            digits = new int[length + 1];//重新分配一个本来长度+1的int数组，里面全是0，也就这一种情况int数组会加长
            digits[0] = 1;

            return digits;
        }
        /// <summary>
        /// 删除排序链表中的重复元素
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        private ListNode DeleteDuplicates(ListNode head)
        {
            ListNode p = head;
            while (p != null)
            {
                if (p.next == null)
                {
                    break;
                }
                int nextval = p.next.val;
                if (p.val == nextval)
                {
                    p.next = p.next.next;
                }
                else
                {
                    p = p.next;
                }
            }
            return head;
        }

        public ListNode deleteDuplicates2(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return head;
            }

            head.next = deleteDuplicates2(head.next);

            return head.val == head.next.val ? head.next : head;
        }

        /// <summary>
        /// 异或方法找不同
        /// 1，位运算解决
        ///这题说的是字符串t只比s多了一个字符，其他字符他们的数量都是一样的，如果我们把字符串s和t合并就会发现
        ///，除了那个多出的字符出现奇数次，其他的所有字符都是出现偶数次。
        /// 一个数和0做XOR运算等于本身：a⊕0 = a
        /// 一个数和其本身做XOR运算等于 0：a⊕a = 0
        /// XOR 运算满足交换律和结合律：a⊕b⊕a = (a⊕a)⊕b = 0⊕b = b
        /// char[][] board = new char[9][];
        //board[0] = new char[9] { '.', '.', '.', '.', '5', '.', '.', '1', '.' };
        //board[1] = new char[9] { '.', '4', '.', '3', '.', '.', '.', '.', '.' };
        //board[2] = new char[9] { '.', '.', '.', '.', '.', '3', '.', '.', '1' };
        //board[3] = new char[9] { '8', '.', '.', '.', '.', '.', '.', '2', '.' };
        //board[4] = new char[9] { '.', '.', '2', '.', '7', '.', '.', '.', '.' };
        //board[5] = new char[9] { '.', '1', '5', '.', '.', '.', '.', '.', '.' };
        //board[6] = new char[9] { '.', '.', '.', '.', '.', '2', '.', '.', '.' };
        //board[7] = new char[9] { '.', '2', '.', '9', '.', '.', '.', '.', '.' };
        //board[8] = new char[9] { '.', '.', '4', '.', '.', '.', '.', '.', '.' };
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private char FindTheDifference(string s, string t)
        {
            char ret = char.MinValue;
            char[] arr = (s + t).ToArray();
            for (int i = 0; i < arr.Length; i++)
            {
                ret ^= arr[i];
            }
            return ret;
        }


        private bool IsValidSudoku(char[][] board)
        {
            bool ret = true;
            //横扫字典
            Dictionary<int, int> dic1 = new Dictionary<int, int>() { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 }, { 9, 0 } };
            //竖扫字典
            Dictionary<int, int> dic2 = new Dictionary<int, int>() { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 }, { 9, 0 } };
            //宫扫字典
            Dictionary<int, int> dic3 = new Dictionary<int, int>() { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 }, { 9, 0 } };
            for (int i = 0; i < 9; i++)
            {
                //按宫扫先求下标，每个宫的第一个格的下标
                // 每个宫的第一个格子是 (036)(036)的9种组合，这个是按宫的外部循环
                //(0,0)(0,3)(0,6)
                //(3,0)(3,3)(3,6)
                //(6,0)(6,3)(6,6)
                int i1 = i - (i % 3);// 012 345 678 要转化成000 333 666，每个都是差0,1,2
                int j1 = (i % 3) * 3;// 012 345 678 要转化成036 036 036
                for (int j = 0; j < 9; j++)
                {
                    if (board[i][j] != '.')//横扫描
                    {
                        int cur = int.Parse(board[i][j].ToString());
                        if (dic1[cur] > 0)
                        {
                            ret = false;
                            break;
                        }
                        else
                        {
                            dic1[cur]++;
                        }
                    }
                    if (board[j][i] != '.')//竖扫描
                    {
                        int cur = int.Parse(board[j][i].ToString());
                        if (dic2[cur] > 0)
                        {
                            ret = false;
                            break;
                        }
                        else
                        {
                            dic2[cur]++;
                        }
                    }

                    int gapi = j / 3;//012 345 678转化成加000 111 222 如(0,3)(0,4)(0,5)(1,3)(1,4)(1,5)(2,3)(2,4)(2,5)
                    int gapj = j % 3;//012 345 678转化成加012 012 012
                    int curi = i1 + gapi;
                    int curj = j1 + gapj;
                    if (board[curi][curj] != '.')//宫扫描
                    {
                        int cur = int.Parse(board[curi][curj].ToString());
                        if (dic3[cur] > 0)
                        {
                            ret = false;
                            break;
                        }
                        else
                        {
                            dic3[cur]++;
                        }
                    }
                }
                //扫完一遍清空字典
                GetNewDic(dic1);
                GetNewDic(dic2);
                GetNewDic(dic3);
            }
            return ret;
        }

        private void GetNewDic(Dictionary<int, int> dic)
        {
            for (int i = 1; i <= 9; i++)
            {
                dic[i] = 0;
            }
        }

        public int StrStr(string haystack, string needle)
        {
            int ret = 0;
            if (string.IsNullOrEmpty(needle))
            {
                return ret;
            }
            int haylength = haystack.Length;
            int needlelength = needle.Length;
            for (int i = 0; i <= haylength - needlelength; i++)
            {
                string curchararr = haystack.Substring(i, needlelength);
                if (curchararr == needle)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 最长公共字符前缀
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public string LongestCommonPrefix(string[] strs)
        {
            string ret = string.Empty;
            if (strs.Length == 0)
            {
                return ret;
            }

            int min = strs[0].Length;
            //先找出最短字符的长度
            for (int i = 1; i < strs.Length; i++)
            {
                string t = strs[i];
                if (t.Length < min)
                {
                    min = t.Length;
                }
            }

            for (int i = 0; i < min; i++)
            {
                char curchar = char.MinValue;
                bool flag = true;
                for (int j = 0; j < strs.Length; j++)
                {
                    string temp = strs[j];
                    if (j == 0)
                    {
                        curchar = temp[i];
                    }
                    else
                    {
                        if (temp[i] != curchar)
                        {
                            flag = false;
                            break;//该字符位置的字符有不一样的
                        }
                    }
                }
                if (!flag)
                {
                    break;//一旦有端的就全部退出
                }
                if (flag)
                {
                    ret += curchar;
                }
            }
            return ret;
        }
        /// <summary>
        /// 数组转链表
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        private ListNode ConvertToNode(string[] arr)
        {
            ListNode nodes = new ListNode() { val = int.Parse(arr[0]) };
            ListNode p = nodes;
            for (int i = 1; i < arr.Length; i++)
            {
                p.next = new ListNode() { val = int.Parse(arr[i]) };
                p = p.next;
            }
            return nodes;
        }

        private string ConvertToString(ListNode nodes)
        {
            string ret = string.Empty;
            if (nodes == null)
            {
                return ret;
            }
            ListNode p = nodes;
            while (p != null)
            {
                int cur = p.val;
                ret = cur + ret;
                p = p.next;
            }
            return ret;
        }

        private string ReverseToString(string n)
        {
            string ret = string.Empty;
            for (int i = 0; i < n.Length; i++)
            {
                ret = n[i] + "," + ret;
            }
            ret = ret.TrimEnd(',');
            return ret;
        }

        private int RemoveElement(int[] nums, int val)
        {
            if (nums.Length == 0)
            {
                return 0;
            }
            int length = nums.Length;
            //int i = 0;
            int j = 0;
            while (j < length)
            {
                //把后面不是val的提上来
                if (nums[j] == val)
                {
                    int havenotval = 0;//记录当前j后面的数字是否有和val不一样的，不一样说明有交换位置的操作
                    for (int k = j; k < length - 1; k++)
                    {
                        if (nums[k + 1] != val)
                        {
                            havenotval++;
                        }
                        int c = nums[k];
                        nums[k] = nums[k + 1];
                        nums[k + 1] = c;
                    }
                    j++;
                    if (nums[j - 1] == val && havenotval != 0)//如果当前交换后的前一个数字仍然是val并且确实交换过，则j回退，说明当前交换后第一个数字还是val，得再次逐个移动后面的数字向前。如果havenotval是0，说明后面的数字全是和val一样的，j不需要做回退操作，任然沿用上面的j++往后移。
                    {
                        j--;
                    }
                    //if (havenotval != 0)
                    //{
                    //    i++;
                    //}
                }
                else
                {
                    j++;//当前快指针如果不等于val则向后移
                }
            }
            int ret = length;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == val)
                {
                    ret--;
                }
            }
            return ret;
        }



        private ListNode AddTwoNumsV2(ListNode l1, ListNode l2)
        {
            ListNode ret = new ListNode();
            ListNode p = ret;
            int carry = 0;//进位
            while (l1 != null || l2 != null)
            {
                int cur1 = l1 != null ? l1.val : 0;
                int cur2 = l2 != null ? l2.val : 0;
                int curtmp = cur1 + cur2;//当前应该加起来等于几
                int cur = (curtmp + p.val) % 10;//当前个位数
                if ((curtmp + p.val) / 10 > 0)
                {
                    carry = 1;
                }
                else
                {
                    carry = 0;
                }
                p.val = cur;
                if ((l1 != null && l1.next != null) || (l2 != null && l2.next != null))
                {
                    p.next = new ListNode() { val = carry };
                    p = p.next;
                }

                if (l1 != null)
                {
                    l1 = l1.next;
                }
                if (l2 != null)
                {
                    l2 = l2.next;
                }
                if (l1 == null && l2 == null && carry == 1)
                {
                    p.next = new ListNode() { val = 1 };
                }

                //if (l1.next == null && l2.next == null)
                //{
                //    break;
                //}
            }
            return ret;
        }

        private bool IsValid(string s)
        {
            Stack<char> st = new Stack<char>();
            Dictionary<char, char> dict = new Dictionary<char, char>() { { ')', '(' }, { '}', '{' }, { ']', '[' } };
            for (int i = 0; i < s.Length; i++)
            {
                if (dict.ContainsValue(s[i]))
                {
                    st.Push(s[i]);//凡是左括号的就直接入栈
                }
                else if (st.Count() == 0)
                {
                    return false;//如果第一个入的是右括号，则直接返回false,说明右括号不能在左括号之前出现
                }
                else if (dict[s[i]] != st.Pop())
                {
                    return false;//如果当前准备入栈的右括号先去通过dic匹配他的左括号和已经在栈里的第一个比较，如果不相等说明没有匹配成功
                }
            }
            return st.Count == 0;//如果栈为空，括号全部配对完，返回true
        }

        public void ReverseString(char[] s)
        {
            int n = s.Length;
            int middle = n / 2;
            for (int i = 0; i < middle;)
            {
                char m = s[i];
                s[i] = s[n - 1];
                s[n - 1] = m;
                i++;
                n--;
            }
        }

        public void reverseStringHelper(char[] s, int left, int right)
        {
            if (left >= right)
                return;
            swap(s, left, right);
            reverseStringHelper(s, ++left, --right);
        }

        private void swap(char[] array, int i, int j)
        {
            char temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        public bool LemonadeChange(int[] bills)
        {
            int dic5 = 0;
            int dic10 = 0;
            for (int i = 0; i < bills.Length; i++)
            {
                if (bills[i] == 5)
                {
                    dic5 += 1;
                }
                else if (bills[i] == 10)
                {
                    if (dic5 > 0)
                    {
                        dic10 += 1;
                        dic5 -= 1;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (bills[i] == 20)
                {
                    if (dic10 > 0 && dic5 > 0)
                    {
                        dic5 -= 1;
                        dic10 -= 1;
                    }
                    else if (dic5 > 2)
                    {
                        dic5 = dic5 - 3;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            return true;
        }


        private int getNum(ListNode node)
        {
            int number = 0;
            int deep = 0;
            int cur = node.val;
            int curnum = deep * 10;
            if (curnum != 0)
            {
                number += curnum;
            }
            else
            {
                number = cur;
            }
            deep++;
            getNum(node.next);
            return number;
        }

    }



    public class KthLargest
    {
        int[] Nums;
        int K;
        public KthLargest(int k, int[] nums)
        {
            this.Nums = new int[nums.Length];
            this.K = k;
            for (int i = 0; i < nums.Length; i++)
            {
                this.Nums[i] = nums[i];
            }
            //Array.Sort(this.Nums);
        }

        public int Add(int val)
        {
            int[] arr = new int[this.Nums.Length + 1];
            for (int i = 0; i < this.Nums.Length; i++)
            {
                arr[i] = this.Nums[i];
            }
            arr[arr.Length - 1] = val;
            this.Nums = new int[arr.Length];
            for (int i = 0; i < this.Nums.Length; i++)
            {
                this.Nums[i] = arr[i];
            }
            Array.Sort(this.Nums);
            return this.Nums[this.Nums.Length - this.K];
        }
    }
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    public class TreeNode
    {
        public int val;
        public TreeNode left { get; set; }
        public TreeNode right { get; set; }

    }

    public class MyDic
    {
        public int MyKey { get; set; }
        public string MyString { get; set; }
    }

    public interface IWorkTaskService
    {
        Task<ApiResult> Algorithm(string s, string s1);

    }


}
