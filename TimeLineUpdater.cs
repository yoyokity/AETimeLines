using System.Text.Json;
using AEAssist;
using AEAssist.Helper;

namespace yoyokity.Common;

public static class TimeLineUpdater
{
    private static List<TimeLineInfo>? jsonData;
    private static string _jsonUrl;

    /// <summary>
    /// 更新时间轴
    /// </summary>
    /// <param name="jsonUrl">json文件链接</param>
    public static async Task UpdateFiles(string jsonUrl)
    {
        //下载json文件
        _jsonUrl = jsonUrl;
        await DownloadJson();
        if (jsonData == null) return;

        //
        var baseDir = Share.CurrentDirectory;
        var triggerlinesDir = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "Triggerlines"));
        if (!Directory.Exists(triggerlinesDir))
        {
            LogHelper.Error("找不到时间轴文件夹");
            LogHelper.Print("时间轴", $"时间轴更新失败！请检查AE的时间轴文件夹是否存在！");
            return;
        }

        var files = Directory.GetFiles(triggerlinesDir, "*.json", SearchOption.TopDirectoryOnly);

        foreach (var timeLine in jsonData)
        {
            var fileName = $"{timeLine.Name}v{timeLine.Version}";
            var filePath = Path.Combine(triggerlinesDir, fileName) + ".json";

            if (!File.Exists(filePath))
            {
                //没有文件直接下载
                if (await DownloadTimeLine(timeLine.DownloadUrl, filePath))
                {
                    LogHelper.Print("时间轴", $"{fileName} 更新成功");
                    if (timeLine.UpdateInfo != "")
                        LogHelper.Print("时间轴", $"更新日志：{timeLine.UpdateInfo}");
                }
                else
                {
                    LogHelper.Print("时间轴", $"时间轴更新失败！请检查github网络连接！");
                    return;
                }
            }
            else
            {
                LogHelper.Print("时间轴", $"{fileName} 已是最新版本");
            }

            //清理旧版本文件
            foreach (var file in files)
            {
                var name = Path.GetFileName(file);
                // 判断文件名是否包含 timeLine.Name 但不包含 timeLine.Version
                if (name.Contains(timeLine.Name) && !name.Contains($"v{timeLine.Version}"))
                    File.Delete(file);
            }
        }
    }

    private static async Task DownloadJson()
    {
        using var client = new HttpClient();
        try
        {
            var _jsonData = await client.GetStringAsync(_jsonUrl);
            jsonData = JsonSerializer.Deserialize<List<TimeLineInfo>>(_jsonData);
        }
        catch (Exception ex)
        {
            jsonData = null;
            LogHelper.Print("时间轴", $"时间轴更新失败！请检查github网络连接！");
        }
    }

    private static async Task<bool> DownloadTimeLine(string url, string path)
    {
        using var client = new HttpClient();
        try
        {
            var jsonData = await client.GetStringAsync(url);
            await File.WriteAllTextAsync(path, jsonData);
            return true;
        }
        catch (Exception ex)
        {
            LogHelper.Error("acr下载时间轴文件时出错");
            return false;
        }
    }
}

public class TimeLineInfo
{
    public string Name { get; set; }
    public string Version { get; set; }
    public string DownloadUrl { get; set; }
    public string UpdateInfo { get; set; }
}
