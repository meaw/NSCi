
drop table serverlocs;



CREATE TABLE `serverlocs` (
 
 `host` bigint(20) DEFAULT NULL,
 
 `ip` text,
  
`lat` double DEFAULT NULL,
 
 `lon` double DEFAULT NULL,
  
`ASN` text,
  
`tag1` text,
  
`tag2` text,
  
`tag3` text,
 
 `tag4` text
) 
ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;



LOAD DATA  LOCAL INFILE 'E:\\LinuxHolder\\AlmostProcessed\\out\\serverlocs.csv' INTO TABLE serverlocs FIELDS TERMINATED BY ',';



select count(*) from serverlocs;




select * from serverlocs;

drop table ip_paths;


CREATE TABLE `ip_paths` (
 
 `srcIP` bigint(20) DEFAULT NULL,
  
`dstIP` bigint(20) DEFAULT NULL,
  
`ssrcIP` text,
  `sdstIP` text,
  
`avg` double DEFAULT NULL,
  
`times` int(11) DEFAULT NULL,
  
`src_lat` double DEFAULT NULL,
  
`src_lon` double DEFAULT NULL,
  
`dst_lat` double DEFAULT NULL,
  
`dst_lon` double DEFAULT NULL,
  
`src_ASN` text,
  
`u0` text,
  
`dst_ASTn` text,
  
`u1` text,
  `
innerAS` int(11) DEFAULT NULL
) 
ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;



LOAD DATA  LOCAL INFILE 'E:\\LinuxHolder\\AlmostProcessed\\out\\IP_PATHS.csv' INTO TABLE ip_paths FIELDS TERMINATED BY ',';


delete from serverlocs where tag2="tag2";

delete from ip_paths where src_ASN="src_ASN";