#Tic Tak Toe Update
class Game:
    def __init__(self):
        #Generate board
        self.board = [' '] * 9
        self.current_turn = 'O'
        self.game_on = False

    #Wipe board
    def clearBoard(self):
        self.board = [' '] * 9
    
    #Print board
    def printBoard(self): print(f'Current Board:\n{self.board[6]}|{self.board[7]}|{self.board[8]}\n-+-+-\n{self.board[3]}|{self.board[4]}|{self.board[5]}\n-+-+-\n{self.board[0]}|{self.board[1]}|{self.board[2]}')
    
    #Print board info
    def printInfoBoard(self): print('Select Move:\n7|8|9\n-+-+-\n4|5|6\n-+-+-\n1|2|3')

    #Prompt user to start the game
    def startGame(self):
        input('Enter anything to start game: ')
        self.clearBoard()
        return True

    #Check whos turn it is
    def runGame(self):
        if self.current_turn == 'O': self.playerMove()
        else: self.botMove()

    #Check if the macth is over
    def gameOver(self):
        #List of all possible wins
        win_check = [self.board[0:3], self.board[3:6], self.board[6:],self.board[0:7:3], \
            self.board[1:8:3], self.board[2::3],  self.board[0::4], self.board[2:7:2]]

        #Check for a win                                                    
        for i in win_check:
            if all(x == i[0] for x in i)  and i[0] != ' ':
                self.printBoard()
                print(f'\nWINNER: {i[0]}')
                self.current_turn = i[0]
                return False
        #Check for a draw
        if ' ' not in self.board:
            self.printBoard()
            print('\nDRAW\n')
            return False
        else: return True
    
    #Prompt player to perform move and executes desired move.
    def playerMove(self):
        self.printBoard()
        print()
        self.printInfoBoard()
        print(f'Current Turn: {self.current_turn}')
        move = input('Select Square: ')
        try: move = int(move)
        except ValueError:
            print('\nError: Please only enter numbers\n')
            return self.playerMove()
        move -= 1
        if move > 8 or move < 0:
            print('\nERROR: please only select numbers between 1 and 9\n')
            return self.playerMove()
        if self.board[move] in 'XO':
            print('\nERROR: Space occupied please try again\n')
            return self.playerMove()
        
        self.board[move] = 'O'
        self.current_turn = 'X'
    
    #The bots moves are decided via a minimax algorithm with alpha beta filtering
    def botMove(self):

        def gameOverBot():
            #List of all possible wins
            win_check = [self.board[0:3], self.board[3:6], self.board[6:],self.board[0:7:3], \
            self.board[1:8:3], self.board[2::3],  self.board[0::4], self.board[2:7:2]]

            for i in win_check:
                if all(x == i[0] for x in i) and i[0] != ' ': return i[0]

            if ' ' not in self.board: return '--'
            else: return None
        
        def max(a, b):
            maxi = -2
            move = None
            results = gameOverBot()
            
            if results == 'O': return [-1, 0]
            elif results == 'X': return [2, 0]
            elif results == '--': return [0, 0]
            
            for i in range(9):
                if self.board[i] == ' ':
                    self.board[i] = 'X'
                    m = min(a, b)

                    if m[0] > maxi:
                        maxi = m[0]
                        move = i

                    self.board[i] = ' '

                    if maxi >= b:
                        return [maxi, move]
                    if maxi > a:
                        a = maxi
                                
            return [maxi, move]
            
        def min(a, b):
            
            mini = 2
            move = None
            results = gameOverBot()

            if results == 'O': return [-1, 0]
            elif results == 'X': return [1, 0]
            elif results == '--': return [0, 0]

            for i in range(9):
                if self.board[i] == ' ':
                    self.board[i] = 'O'
                    m = max(a, b)

                    if m[0] < mini:
                        mini = m[0]
                        move = i
                    
                    self.board[i] = ' '

                    if mini <= a:
                        return [mini, move]
                    if mini < b:
                        b = mini
            
            return [mini, move]
        
        r = max(-2, 2)
        self.board[r[1]] = 'X'
        self.current_turn = 'O'

#Loop the game to allow rematches.
game = Game()

while True:
    game.game_on = game.startGame()
    while game.game_on:
        game.runGame()
        game.game_on = game.gameOver()
        