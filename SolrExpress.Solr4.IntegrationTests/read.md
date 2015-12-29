### Steps to make this tests work:
1. Access http://archive.apache.org/dist/lucene/solr/4.9.0/
2. Download solr-4.9.0.zip file
3. Unzip file in C:\Temp (or other folder of your preference)
4. Make sure than all requirements of SOLR system was installed in computer (basically, Java JRE 1.5+)
5. Open a command prompt and then write bellow commands:
	1. cd C:\Temp\solr-4.9.0\example
	2. java -jar start.jar
6. Open another command prompt and then write bellow commands:
	1. cd C:\Temp\solr-4.9.0\example\exampledocs
	2. java -jar post.jar *.xml
7. If universe likes you and all things are right in your environment, you will be able to access http://localhost:8983/solr/#/collection1 and start these tests :)
8. To shutdown SOLR server, just go to first command prompt and press CTRL+C