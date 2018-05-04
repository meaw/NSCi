#install.packages("tcltk2")
#install.packages("rgl")

#tell R we'll be using Igraph
library(igraph)
library(fitdistrplus)
SCRDIR="E:\\LinuxHolder\\AlmostProcessed\\temp\\"


g_06<-read_graph(paste(SCRDIR, "internet06.gml", sep=""), format = c("gml"))



g_03<-read_graph(paste(SCRDIR, "graph.gml", sep=""), format = c("gml"))





summary_table <- data.frame("Network Name"= character(), "Type (1=directed, 0=undirected)"=integer(), "N Number of nodes"=integer(), "M Number of links"=integer(), "c # of connected components"=integer(),"d maximum degree"=integer(),"l average pathlength"=numeric(),"L Diameter"=numeric(),"CCl Avg. Local Cluster Coeff."=numeric(),"CCg Global clustering coeff."=numeric(), stringsAsFactors=FALSE,check.names = FALSE)
summary_table[nrow(summary_table) + 1,]  <-c("Internet"        ,is_directed(g_06)*1, vcount(g_06), ecount(g_06),count_components (g_06),max(degree(g_06)),mean_distance(g_06, directed = TRUE, unconnected = TRUE) ,diameter(g_06, directed = TRUE, unconnected = TRUE, weights = NULL) , transitivity(g_06, type="localaverageundirected") ,    transitivity(g_06, type="globalundirected")   )   
summary_table[nrow(summary_table) + 1,]  <-c("Internet"        ,is_directed(g_03)*1, vcount(g_03), ecount(g_03),count_components (g_03),max(degree(g_03)),mean_distance(g_03, directed = TRUE, unconnected = TRUE) ,diameter(g_03, directed = TRUE, unconnected = TRUE, weights = NULL) , transitivity(g_03, type="localaverageundirected") ,    transitivity(g_03, type="globalundirected")   )   
summary_table





#time to plot the graphs structure


#for details on plot see http://igraph.org/r/doc/plot.common.html
#save the resulting plots to an image 



#plot G 06
png(filename = "g_06.png",   width = 6000, height = 6000,bg=NA, units = "px",res=600, antialias="cleartype")
set.seed(24)
plot(g_06,vertex.size=log(degree(g_06)+1)*2.5+2,vertex.color='red',vertex.label.cex=.1,vertex.label.color = "black",edge.arrow.size=.2,edge.width=.2,layout=layout.auto)
dev.off()


#plot G 03
png(filename = "g_03.png",   width = 6000, height = 6000,bg=NA, units = "px",res=600, antialias="cleartype")
set.seed(24)
plot(g_03,vertex.size=log(degree(g_03)+1)*2.5+2,vertex.color='red',vertex.label.cex=.1,vertex.label.color = "black",edge.arrow.size=.2,edge.width=.2,layout=layout.auto)
dev.off()



#part 2

png(filename = "g_06_dist.png",   width = 4800, height = 4800,bg=NA, units = "px",res=600, antialias="cleartype")
plot(degree_distribution(g_06,cumulative = FALSE),main="Internet", xlab="Degree (k)", ylab="P(k)",log="xy")
legend("topright", legend=c("Degree distribution "), col=c("black"),pch = c(1), cex=1.0)
dev.off()




png(filename = "g_03_dist.png",   width = 4800, height = 4800,bg=NA, units = "px",res=600, antialias="cleartype")
plot(degree_distribution(g_03,cumulative = FALSE),main="Internet", xlab="Degree (k)", ylab="P(k)",log="xy")
legend("topright", legend=c("Degree distribution "), col=c("black"),pch = c(1), cex=1.0)
dev.off()



#part 3 compute the pathlenght histogram

png(filename = "g_06_hist.png",   width = 4800, height = 4800,bg=NA, units = "px",res=600, antialias="cleartype")
yval=path.length.hist(g_06)$res
tot=sum(yval)
yval=yval*(1/tot)
xp <- seq(1,length(yval), by = 1)
barplot(yval,names.arg=xp,main="Internet links path histogram", xlab="l (path length)", ylab="Number of occurrances")
legend("topright", legend=c("Min. path length "), fill=c("gray"),bty = c(1), cex=1.0)
dev.off()


png(filename = "g_03_hist.png",   width = 4800, height = 4800,bg=NA, units = "px",res=600, antialias="cleartype")
yval=path.length.hist(g_03)$res
tot=sum(yval)
yval=yval*(1/tot)
xp <- seq(1,length(yval), by = 1)
barplot(yval,names.arg=xp,main="Internet links path histogram", xlab="l (path length)", ylab="Number of occurrances")
legend("topright", legend=c("Min. path length "), fill=c("gray"),bty = c(1), cex=1.0)
dev.off()

