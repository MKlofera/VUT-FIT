'''
* Project: interpret.py
* 2021/2022
* @author Marek Klofera <xklofe01@stud.fit.vutbr.cz> - VUT FIT student
'''

from interpret_libraries.ErrorHandler import errorHandler, successExit

class InstructionList:

    def __init__(self):
        super().__init__()
        self.list = []
        self.instructionCount = 0
        self.execInstNum = 0
        self.labels = {}
        self.jumpsNumber = 0
        self.jumpTo = None
        self.callStack = []


    def addInstruction(self, instruction):
        self.list.append(instruction)

    def setLabels(self):
        '''
        before precesing any instruction set all labels for possible jumps.
        Function will find all instructions with opcode="LABEL" and "CALL" (call has also label)
        '''
        for instructionNumber, instruction in enumerate(self.list):
            if (instruction.name == "LABEL"):
                labelName = instruction.args[0]["data"]
                if(labelName in self.labels):
                    errorHandler(52, "instructionList.py", "setLabels", "trying to create already existating label")
                else:
                    self.labels[labelName] = instructionNumber

    def getInstruction(self):
        if(self.jumpTo != None):
            for labelName in self.labels:
                if (labelName == self.jumpTo):
                    self.execInstNum = self.labels[labelName]
                    nextInstruction = self.list[self.execInstNum]
                    self.execInstNum += 1
                    self.jumpTo = None
                    self.jumpsNumber += 1
                    maximumAllowedJumps = 300
                    if self.jumpsNumber > maximumAllowedJumps:
                        errorHandler(99 , "InstructionList", "getInstruction" "too many jumps ")

        elif(self.execInstNum >= len(self.list)):
            # no instruction to process
            successExit("InstructionList", "getInstruction", "no instruction to process")
        else:
            nextInstruction = self.list[self.execInstNum]
            self.jumpsNumber = 0
            self.instructionCount += 1
            self.execInstNum += 1

        return nextInstruction




