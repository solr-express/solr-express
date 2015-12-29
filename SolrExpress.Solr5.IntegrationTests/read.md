### Steps to make this tests work:
1. Access http://archive.apache.org/dist/lucene/solr/5.3.1/
2. Download solr-5.3.1.zip file
3. Unzip file in C:\Temp (or other folder of your preference)
4. Make sure than all requirements of SOLR system was installed in computer (basically, Java JRE 1.7+)
5. Open a command prompt and then write bellow commands:
	1. cd C:\Temp\solr-5.3.1\bin
	2. solr start -e techproducts -noprompt
6. If universe likes you and all things are right in your environment, you will be able to access http://localhost:8983/solr/#/techproducts and start these tests :)
7. To shutdown SOLR server, just open a command prompt and write command "solr stop -all"