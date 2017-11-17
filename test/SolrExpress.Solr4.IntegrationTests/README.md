### Tests setup
1. Install Solr (4.9.0)
	1. Access http://archive.apache.org/dist/lucene/solr/4.9.0/
	2. Download solr-4.9.0.zip file
	3. Unzip file in C:\Temp (or another folder of your preference)
	4. Make sure than all requirements of SOLR system was installed in computer (basically, Java JRE 1.5+)
2. Start solr executing
	```powershell
	cd C:\Temp\solr-4.9.0\example

	java -jar start.jar
	```
3. Populate Solr collection executing (in other command prompt) 
	```powershell
	cd C:\Temp\solr-4.9.0\example\exampledocs

	java -jar post.jar *.xml
	```
4. Test Solr collection accessing http://localhost:8983/solr/#/collection1
5. Start unit tests in this project