### Tests setup
1.  Install Solr (5.5.0)
	1. Access http://archive.apache.org/dist/lucene/solr/5.5.0/
	2. Download solr-5.5.0.zip file
	3. Unzip file in C:\Temp (or another folder of your preference)
	4. Make sure than all requirements of SOLR system was installed in computer (basically, Java JRE 1.7+)

2.  Start and populate solr executing
	```powershell
	cd C:\Temp\solr-5.5.0\bin

	solr start -e techproducts -noprompt
	```
3.  Test Solr collection accessing http://localhost:8983/solr/#/techproducts
4.  Start unit tests in this project