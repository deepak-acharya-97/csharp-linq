f=open('debug.log','r')
rankGeneration=0
suitGeneration=0

for i in f.readlines():
    if 'Rank Generation' in i:
        rankGeneration+=1
    elif 'Suit Generation' in i:
        suitGeneration+=1

print('Rank Generation ',rankGeneration)
print('Suit Generation ',suitGeneration)