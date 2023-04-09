# Mykey

## 依赖
.net7 运行时。若没有安装环境，则第一次启动将会自动打开环境安装网页，下载并安装即可。

## 使用说明
![image](https://user-images.githubusercontent.com/10357789/230761169-f1c47082-4140-436c-ac50-c2cc68d7c85e.png)

点击开始/停止旁边的按钮，再按下对应按键，即可绑定开始/停止按键精灵的热键。

按键列表中可输入多个按键，包括F1~F12、LM（鼠标左键点击）、RM（鼠标右键点击）。暂时不支持Ctrl、Shift等修饰键。当输入多个按键时，需要用`|`分割，例如`a|b|c|F10`，那么启动后会按照`a`、`b`、`c`、`F10`的次序依次按下，并在结束一轮后重新开始，无限循环按键。

按键间隔以毫秒为单位，1000为1秒。尽量不要输入太小，如0。

## 工作原理
使用TS驱动模拟按键，是前台按键，不能在后台进行按键，也没有后台按键的开发计划。
