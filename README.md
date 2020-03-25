youtube-watch-history-converter
===============================
_Convert watch history from YouTube to JSON Lines format. Part of a show-through that leads to [this Datastudio report](https://datastudio.google.com/open/1ROnOMEHsWCYOU9pK4h_pJecr_qaF5oFw)._

Exporting from Google Takeout
-----------------------------
[takeout.google.com](https://takeout.google.com/)

1. Export YouTube data. Choose "Logg"/"History" and JSON as format.
2. When export is done, download the results and find "watch-history.json"

Converting data to JSON Lines
-----------------------------
[github.com/teodoran/youtube-watch-history-converter](https://github.com/teodoran/youtube-watch-history-converter)

1. Clone repo and build with `dotnet build`
2. Then convert "watch-history.json" with the following.

```shell
$ HistoryConverter> dotnet run -- ../Exports/watch-history.json
```

Creating a new BigQuery dataset and table
-----------------------------------------
[console.cloud.google.com](https://console.cloud.google.com/bigquery?project=computas-nxt-youtube-analyse)

1. From the UI create a new dataset. Prefix it with CX-initials.
2. Create a new table and upload the converted history.

Finding an outlier
------------------

From the BigQuery UI, hunt for some outliers. A good start is to group by title:
```sql
SELECT COUNT(Title), Title
FROM `computas-nxt-youtube-analyse.tae_youtube_data.views`
GROUP BY Title
ORDER BY COUNT(Title) DESC
LIMIT 100
```

You might identify two strange cases. Let's have a closer look:
```sql
SELECT *
FROM `computas-nxt-youtube-analyse.tae_youtube_data.views`
WHERE Title = 'Så en video som er fjernet'
-- WHERE Title = 'Så på https://www.youtube.com/watch?v=Uwo1KGDVSEk'
-- AND Id IS NOT NULL
LIMIT 100
```

Make a BigQuery view
--------------------
Create a view based on the outlier findings that filters out unwanted views. You might end up with something along the lines of:
```sql
SELECT *
FROM `computas-nxt-youtube-analyse.tae_youtube_data.views`
WHERE Id IS NOT NULL
AND ChannelUrl IS NOT NULL
```

Enter Datastudio
----------------
[datastudio.google.com](https://datastudio.google.com)

1. From the Datastudio UI, create a new data source.
2. Connect to BigQuery and the view you made.
3. Create two custom properties: "No of Channels" `COUNT_DISTINCT(ChannelUrl)` and "No of Videos" `COUNT_DISTINCT(Id)`
4. Now create a report. Explore different graphs and filters
5. Try to copy the [YouTube History report](https://datastudio.google.com/open/1ROnOMEHsWCYOU9pK4h_pJecr_qaF5oFw) and update the data binding.
