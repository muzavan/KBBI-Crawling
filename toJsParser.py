import MySQLdb as mdb
import json
import db_config as db
import collections

def parsing():
	"""Parsing Data KBBI from Database (Crawled Data) into JSON so can be used in Static Web. Crawled Nov, 1 2014. Beware of any updates from kbbi.web.id"""
	query = 'SELECT * FROM kbbi'
	cnx = mdb.connect(db.HOST,db.USERNAME,db.PASSWORD,db.DB_NAME)
	cur = cnx.cursor(mdb.cursors.DictCursor)
	cur.execute(query)
	rows = cur.fetchall()
	result = collections.OrderedDict()
	for row in rows:
		result[str(row['kata'])] = str(row['arti'])

	f = open('kamus.json','w')
	json.dump(result,f)


if __name__ == '__main__':
	parsing()
