select * from statfactures where (taxe1<>0 or taxe2<>0) and typefacture<>'Physiothérapie: Absence non motivée'

select * from statfactures where nopret<>0 or novente<>0
select * from statpaiements where nopret<>0 or novente<>0
select * from ventes 
 
delete from statfactures where nopret<>0 or novente<>0 or nofacture in (192444,192445,192446)
delete from statpaiements where nopret<>0 or novente<>0 or nofacture in (192444,192445,192446)
delete from factures where nopret<>0 or novente<>0 or nofacture in (192444,192445,192446)
delete from ventes
delete from prets


delete from statfactures where nokp=138
delete from statpaiements where nokp=138
delete from factures where nokp=138


