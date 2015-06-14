### Steps to make this tests work:
1. Access http://archive.apache.org/dist/lucene/solr/4.9.0/
2. Download the solr-4.9.0.zip file
3. Unzip the file in C:\Temp (or other folder of your preference)
4. Make sure than all requirements of SOLR system was installed in the computer (basically, Java JRE 1.5+)
5. Open the command prompt and then write the bellow commands:
	1. cd C:\Temp\solr-4.9.0\example
	2. java -jar start.jar
6. Open another command prompt and then write the bellow commands:
	1. cd C:\Temp\solr-4.9.0\example\exampledocs
	2. java -jar post.jar *.xml
6. If the universe like you, all the things is right and you can access http://localhost:8983/solr/#/collection1, you can  and start these tests :)
7. To shutdown SOLR server, just go to the first command prompt and press CTRL+C