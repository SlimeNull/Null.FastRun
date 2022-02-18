# Null.Faststart

一个快捷工具集管理工具, 设定从名称到路径的映射, 程序将自动帮你创建符号链接, 并添加到 PATH 环境变量, 至此, 你便可以用通过 '运行对话框' 或着其他任何东西直接访问到你的程序, 文件或目录

![](imgs/main.png)


## 说明

程序以 yaml 格式存储配置信息, 默认的配置文件包含以下内容:
```yaml
links_path: C:\WINDOWS\NFaststart
links_mode: Symbolic
links:
```

添加一个映射, 可以使用程序编辑, 也可以直接在 links 项下添加键值对:
```yaml
links:
  me: C:\Users\SlimeNull
  translate: C:\NShare\Programs\Translator.py
```

在程序应用配置时, 将会做以下行为:
1. 保存配置
2. 清空旧的软链接并创建新的
3. 检查 PATH 环境变量, 并应用

> 这意味这, 如果您直接通过配置文件更改 links_path 节点, 旧的路径会残留在 PATH 环境变量中, 不要这么做

如果你已经应用过配置, 并且希望更改 links_path
1. 请打开配置(Config)窗口
2. 点击卸载(Uninstall), 并确认.

> 该操作会删除程序在 PATH 应用的路径, 并删除软链接目录.

## 使用



你可以直接编译 Null.Faststart.WinForm 项目(必须使用 Release), 并使用编译后的程序.

程序需要管理员权限来操作系统环境变量以及创建软链接, 如若程序运行时未被赋予管理员权限, 则会报错并退出.

在程序主界面

## 动态链接库
程序的核心在 NullLib.Faststart 项目中, 该项目包含访问系统环境变量以及使用 WinAPI 创建软链接的逻辑.
Null.Faststart.WinForm 是管理器的实现, 它将用户操作简化为 GUI 操作