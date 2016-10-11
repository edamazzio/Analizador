def suma(a,b):
    a = a + b
    for x in range (0,5):
    	a = a * b
    	if (b<a):
    		b = b*b
    		for x in range (0,5):
		    	a = a * b
		    	a = suma(a, a*a)
    for x in range (0,5):
    	a = a * b
    	if (b<a):
    		b = b*b
    return a
