# 学习Markdown的第一天：
## 编写工具：VSCode
### VSCode是现代非常受欢迎的一款代码编辑器
#### VSCode使用非常方便且高效
##### 对于各种不同的语言都具有很好的适应性
###### 其中VSCode中的众多插件更是打开了VSCode的多样性与可玩性

# 使用VSCode令人非常愉悦 

*各种各样适应与不同语言的插件以及主题、图标与代码高亮标识等插件让人非常愉悦*
_很上瘾_
**非常NICE**
__不怎么NICE__


-Item 1
-Time 2
-Time 3

__Zlim Like:__
1.Girl
2.Money
3.YoonALim

<!-- http://simpledesktops.com/browse/41/ - 简约的桌面壁纸 -->
[简约的桌面壁纸](http://simpledesktops.com/browse/41/)


##### _正如 Kanye West 所说_：
>_We're living the future so_ 
>_the present is out past._

---
* __C语言__
___
* __Shell__
___
* # Enough for today.

___
# AVL树：本质是自平衡的二叉搜索树
## 平衡：即某一个结点的左右子树的高度越接近越好
## 平衡因子：结点的左子树高度减去右子树高度
### AVL树所有结点的平衡因子的绝对值不超过1

__n   ：node__
__p   ：parent__
__G   ：grandparent__


### __对某个结点进行左旋转：表示让此结点的右结点等于其右子结点的左子结点，并让其右子结点的左子结点指向其本身__

### __对某个结点进行右旋转：表示让此结点的左结点等于其左子结点的右子结点，并让其左子节点的右子结点指向其自身__

## 左旋转：待旋转结点（grand），旋转节点的右子结点（parent = grand.right），右子结点的左子结点（child = parent.left）
## grand.right = parent.left;
## parent.left = grand;
## 右旋转：待旋转结点：grand，待旋转结点的左子结点：parent = grand.left：
## grand.left = parent.right;
## parent.right = grand;

### LR：实际上对于根结点来说进行了LRR三次比较：故对于根结点的左子结点来说，相当于进行了一次RR，故先需要对根结点的左子结点进行左旋转使根结点的左子树部分恢复平衡，而后再对根节点进行右旋转使整颗树恢复平衡 

### RL：实际对于根结点来说进行了RLL三次比较：故对于根结点的右子结点来说，相当于进行了一次LL，故需要先对根结点的右子结点进行右旋转使其恢复平衡，而后对根结点进行左旋转使整颗树恢复平衡





