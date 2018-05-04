#install.packages("tcltk2")
#install.packages("rgl")

#tell R we'll be using Igraph
library(igraph)
library(jsonlite)
library(fitdistrplus)
SCRDIR="E:\\LinuxHolder\\AlmostProcessed\\temp\\"



g_06<-read_graph(paste(SCRDIR, "internet06.gml", sep=""), format = c("gml"))
g_03<-read_graph(paste(SCRDIR, "graph.gml", sep=""), format = c("gml"))

gs<- list(g_03,g_06)
gs_names<- list("Internet'17","Internet'06")

#summary_table <- data.frame("Network Name"= character(), "max_deg"=integer(), "node deg"=list(), "max_ecc"=integer(), "node ecc"=list(), "max_clo"=integer(), "node clo"=list(), "max_bet"=integer(), "node bet"=list(), "max_pr"=integer(), "node pr"=list(), "max_auth"=integer(), "node auth"=list(), "max_hub"=integer(), "node hub"=list())
summary_table <- data.frame("Network Name"= character(), "max_deg"=numeric(), "node deg"=integer(), "max_ecc"=numeric(), "node ecc"=integer(), "max_clo"=numeric(), "node clo"=integer(), "max_bet"=numeric(), "node bet"=integer(), "max_pr"=numeric(), "node pr"=integer(), "max_auth"=numeric(), "node auth"=integer(), "max_hub"=numeric(), "node hub"=integer())

for (i in 1:2){
  tag=gs_names[[i]]
  max_deg=  max(degree(gs[[i]]))
  node_deg=  V(gs[[i]])[degree(gs[[i]])==max(degree(gs[[i]]))]
  
  max_ecc=  max(eccentricity(gs[[i]]))
  node_ecc=  V(gs[[i]])[eccentricity(gs[[i]])==max(eccentricity(gs[[i]]))]
  
  max_clo=  max(closeness(gs[[i]]))
  node_clo=  V(gs[[i]])[closeness(gs[[i]])==max(closeness(gs[[i]]))]
  
  
  max_bet=  max(betweenness(gs[[i]]))
  node_bet=  V(gs[[i]])[betweenness(gs[[i]])==max(betweenness(gs[[i]]))]
  
  max_pr=  max(page_rank(gs[[i]])$vector)
  node_pr=  V(gs[[i]])[page_rank(gs[[i]])$vector==max(page_rank(gs[[i]])$vector)]
  
  max_auth=  max(authority_score(gs[[i]])$vector)
  node_auth=  V(gs[[i]])[authority_score(gs[[i]])$vector==max(authority_score(gs[[i]])$vector)]
  
  max_hub=  max(hub_score(gs[[i]])$vector)
  node_hub=  V(gs[[i]])[hub_score(gs[[i]])$vector==max(hub_score(gs[[i]])$vector)]
  
  summary_table[nrow(summary_table) + 1,]  <-c(tag,max_deg,toJSON(as.vector(node_deg)),max_ecc,toJSON(as.vector(node_ecc)),max_clo,toJSON(as.vector(node_clo)),max_bet,toJSON(as.vector(node_bet)),max_pr,toJSON(as.vector(node_pr)),max_auth,toJSON(as.vector(node_auth)),max_hub,toJSON(as.vector(node_hub)))
  
}

summary_table
write.csv(summary_table, "out.csv")











summary_table2 <- data.frame("Network Name"= character(), "n"=numeric(), "m"=numeric(), "dmin"=numeric(), "dmax"=numeric(), "l"=numeric(), "D"=numeric(), "ccg"=numeric(), "E2"=numeric(), "En"=numeric(),Eavg=numeric(),Eaux=numeric())
gs_names<- list("Internet'17","Internet'06")
gs<- list(g_03,g_06)

m1<- list()

mdiag<- list()
for (i in 1:4){
  tag=gs_names[[i]]
  n=  vcount(gs[[i]])
  m=  ecount(gs[[i]])
  
  dmin=min(degree(gs[[i]]))
  dmax=max(degree(gs[[i]]))
  davg=sum(degree(gs[[i]]))/n
  
  l= mean_distance(gs[[i]], directed = TRUE,  unconnected = TRUE)
  #If TRUE only the lengths of the existing paths are considered and averaged; 
  d=diameter(gs[[i]], directed = TRUE, unconnected = TRUE, weights = NULL)
  ccg= transitivity(gs[[i]], type = "globalundirected")
  
  
  m0=as_adj(gs[[i]], type="both",names=FALSE)
  m1[[i]] <- as.matrix(m0)
  pdiag=degree(gs[[i]])
  mdiag[[i]]=diag(n)*pdiag
  
  LM=eigen(m1[[i]])$values
  LM=eigen_centrality(gs[[i]],scale=FALSE)$value
  
  #XM=abs(LM)
  #result=sort(XM, index=TRUE)$ix
  #SM=LM[result[2]]
  SM=eigen(mdiag[[i]]-m1[[i]])$values
  auxm=sum(eigen(mdiag[[i]]-m1[[i]])$values)
  summary_table2[nrow(summary_table2) + 1,]  <-c(tag,n,m,dmin,dmax,l,d,ccg,SM[n-1],LM[1],davg,auxm)
  
}

summary_table2
write.csv(summary_table2, "out2.csv")



