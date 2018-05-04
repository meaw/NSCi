SELECT * FROM asniot.ip_paths where (dst_ASTn='{1299}' or src_ASN='{1299}') and innerAS=1;

SELECT count(*) FROM asniot.ip_paths where (dst_ASTn='{1299}' or src_ASN='{1299}') and innerAS=1;
SELECT avg(`avg`) FROM asniot.ip_paths where (dst_ASTn='{1299}' or src_ASN='{1299}') and innerAS=1;

SELECT count(*) FROM asniot.ip_paths where (dst_ASTn='{1299}' or src_ASN='{1299}') and innerAS=0;
SELECT avg(`avg`) FROM asniot.ip_paths where (dst_ASTn='{1299}' or src_ASN='{1299}') and innerAS=0;



SELECT count(*) FROM asniot.ip_paths where (dst_ASTn='{3356}' or src_ASN='{3356}') and innerAS=1;
SELECT avg(`avg`) FROM asniot.ip_paths where (dst_ASTn='{3356}' or src_ASN='{3356}') and innerAS=1;

SELECT count(*) FROM asniot.ip_paths where (dst_ASTn='{3356}' or src_ASN='{3356}') and innerAS=0;
SELECT avg(`avg`) FROM asniot.ip_paths where (dst_ASTn='{3356}' or src_ASN='{3356}') and innerAS=0;


SELECT count(*) FROM asniot.ip_paths where (dst_ASTn='{6939}' or src_ASN='{6939}') and innerAS=1;
SELECT avg(`avg`) FROM asniot.ip_paths where (dst_ASTn='{6939}' or src_ASN='{6939}') and innerAS=1;

SELECT count(*) FROM asniot.ip_paths where (dst_ASTn='{6939}' or src_ASN='{6939}') and innerAS=0;
SELECT avg(`avg`) FROM asniot.ip_paths where (dst_ASTn='{6939}' or src_ASN='{6939}') and innerAS=0;



SELECT * FROM asniot.serverlocs where lat!=-999 and lon!=-999  and ASN = '{1299}' and tag1 like '%%';




SELECT count(*) FROM asniot.ip_paths where innerAS=1;

SELECT count(*) FROM asniot.ip_paths where innerAS=0;