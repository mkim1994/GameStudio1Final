import music21
import sys

music21.environment.set('autoDownload', 'allow')


fileURL1 = "http://kern.humdrum.org/cgi-bin/ksdata?file=penta"
fileURL2 = ".krn&l=users/craig/songs/pentatonic&format=kern"

def translateNoteToIndex(note):
	if note == 'C3':
		return 0
	elif note == 'D3':
		return 1
	elif note == 'E3':
		return 2
	elif note == 'G3':
		return 3
	elif note == 'A3':
		return 4
	elif note == 'C4':
		return 5
	elif note == 'D4':
		return 6
	elif note == 'E4':
		return 7
	elif note == 'G4':
		return 8
	elif note == 'A4':
		return 9
	else:	
		return 0

		
w, h = 10, 10
markovTransitionMatrix = [[1 for x in range(w)] for y in range(h)]

for id in range(1,68):
	tensDigit = ''
	if id < 10:
		tensDigit = '0'
	
	url = fileURL1 + tensDigit + str(id) + fileURL2

	fileScore = music21.converter.parse(url)
	keyGuess = fileScore.analyze('key')
	majorKey = keyGuess.asKey('major')
	transposeInterval = music21.interval.Interval(majorKey.tonic, music21.pitch.Pitch('C'))
	transposedScore = fileScore.transpose(transposeInterval)
	coupledNotesList = list()
	lastEl = transposedScore.recurse().notes[0]
		
	for el in transposedScore.recurse().notes:
		coupledNotesList.append((translateNoteToIndex(lastEl.nameWithOctave), translateNoteToIndex(el.nameWithOctave)))
		lastEl = el

	coupledNotesList.pop(0)

	for couple in coupledNotesList:
		markovTransitionMatrix[couple[0]][couple[1]] += 1


transitionMatrixString = ''

for row in range(w):
	for col in range(h):
		transitionMatrixString += str(markovTransitionMatrix[row][col])
		if col != h - 1:
			transitionMatrixString += ','
	if row != w - 1:
		transitionMatrixString += '\n'

print(transitionMatrixString)
f = open('markovChainTransitionMatrix.txt', 'w')
f.write(transitionMatrixString)
f.close()

