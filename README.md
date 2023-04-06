# Mykey

## 依赖
.net7 运行时。

## 使用说明
打开可执行文件，稍等片刻，可以在同级文件夹下看到config.json，用编辑器打开它。因为没做可视化的编辑器，所以直接编辑JSON。

```json
{
    "Configs": [
        {
            "ConfigName": "Example",
            "Interval": 1000,
            "PressKey": "F10"
        }
    ],
    "CurrentIndex": 0,
    "HotKey": "F7"
}
```

`CurrentIndex`: 当前选择的方案索引位置。

`HotKey`: 切换开始、结束的按键名，名称必须在 [HotKey.cs](https://github.com/dicarne/Mykey/blob/main/Mykey/HotKey.cs) 中的Keys中有。

`Configs`: 保存多个配置方案，可切换。

`ConfigName`: 方案名称。

`Interval`: 按键间隔，单位毫秒。

`PressKey`: 按下的按键，可以填写多个，用`|`分割开来。在运行时会依次按下。

## 工作原理
使用TS驱动模拟按键，是前台按键，不能在后台进行按键，也没有后台按键的开发计划。
