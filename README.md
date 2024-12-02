本仓库用于存放AE的acr时间轴，便于统一版本管理与自动更新



# 使用说明

- 找yoyokity要个仓库权限，给个github的邮箱。
- 新建个作者的文件夹和`Master.json`，前者用于存放时间轴文件，后者用于更新验证与记录版本信息。
- 将写好的时间轴放进文件，注意文件名里不要加版本号，不然你下载地址要一直改麻烦得很。
- 后续要更新时间轴直接在github网页上传覆盖或者fork仓库都行。



## Master文件说明

- **Name**：时间轴文件名

​		文件名不要包含版本号，对应时间轴文件也应该是 `时间轴文件名.json`

- **Version**：时间轴版本号

​		更新逻辑是检测本地时间轴文件夹中，是否包含了Name相同且Version相同的文件。

​		如果没有则从仓库下载最新文件，同时删除本地文件夹中Name相同且Version不同的文件。

- **DownloadUrl**：时间轴下载url

```
https://raw.githubusercontent.com/yoyokity/AETimeLines/refs/heads/master/作者文件夹名/时间轴文件名.json
```

- **UpdateInfo**：更新信息



## acr模块

- 将`TimeLineUpdater.cs`放到你的acr中，改一下namespace就行

- 要更新就调用`UpdateFiles(url)`方法，`url`是你的Master文件链接

```
https://raw.githubusercontent.com/yoyokity/AETimeLines/refs/heads/master/你的Master.json
```

