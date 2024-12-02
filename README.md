本仓库用于存放AE的acr时间轴，便于统一版本管理与自动更新



## 使用说明

  1. **获取权限**
     向 **yoyokity** 请求仓库访问权限，并提供你的 GitHub 邮箱。

  2. **文件结构**

     为每个作者创建一个专属文件夹，并在其中创建`Master.json`文件。

     > `Master.json` 用于存放版本信息和更新验证。
     >
     > 作者的文件夹用于存放实际的时间轴文件。

  3. **上传时间轴文件**

     将编写好的时间轴文件放入作者文件夹中。

     > **注意**：文件名不要包含版本号，避免频繁更新时需要修改下载链接。

  4. **更新时间轴文件**

     直接在 GitHub 网页上传覆盖旧文件，或者通过 Fork 仓库的方式进行更新。



## Master.json 文件说明

- **Name**：时间轴文件名

​		文件名不要包含版本号，对应的时间轴文件名应该是 `时间轴文件名.json`

- **Version**：时间轴版本号

  版本号用于版本控制和更新检测。更新逻辑如下：

  - 检查本地时间轴文件夹中是否存在与 `Name` 相同且 `Version` 相同的文件。
  - 如果没有该文件，系统将自动下载最新版本。
  - 下载后，会删除本地文件夹中其他任何`Name` 相同且 `Version` 不同的文件。

- **DownloadUrl**：时间轴下载链接

```
https://raw.githubusercontent.com/yoyokity/AETimeLines/refs/heads/master/作者文件夹名/时间轴文件名.json
```

- **UpdateInfo**：更新信息



## acr模块

- 将 `TimeLineUpdater.cs` 文件复制到你的项目中，并修改 `namespace` 名称。

- 调用 `UpdateFiles(url)` 方法来更新时间轴文件，`url` 是指向 `Master.json` 文件的链接。

```
https://raw.githubusercontent.com/yoyokity/AETimeLines/refs/heads/master/你的Master.json
```

