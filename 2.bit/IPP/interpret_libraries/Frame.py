'''
* Project: interpret.py
* interpreting IPPcode22
* 2021/2022
* @author Marek Klofera <xklofe01@stud.fit.vutbr.cz> - VUT FIT student
'''

from interpret_libraries.Variable import Variable
from interpret_libraries.ErrorHandler import errorHandler
import re

class Frame:

    def __init__(self):
        super().__init__()
        self.globalFrame = {}
        self.localFrame = {}
        self.frameStack = []
        self.temporaryFrame = {}
        self.temporaryFrameisDefined = False
        self.localFrameisDefined = False

        self.localFrameStorage = []
    def addToFrame(self, variable):
        '''
        :param variable: instance of Variable class
        :return: None
        '''
        if (variable.frame == "TF" and not self.temporaryFrameisDefined):
            errorHandler(55, "frame.py", "addToFrame", "non-existing temporary frame")

        if (variable.frame == "LF" and not self.localFrameisDefined):
            errorHandler(55, "frame.py", "addToFrame", "non-existing temporary frame")
        try:
            rightFrameToAssign = self.getSpecificFrame(variable.frame)
            rightFrameToAssign[variable.name] = variable
        except Exception as e:
            errorHandler(99, "frame.py", "addToFrame")

    def assignVariable(self, data, newType: str, newValue: str):
        '''
        :param data: string 'GF|LF|TF@someName' or instance of Variable class
        :param newType: string
        :param newValue:
        :return:
        '''
        try:
            if (newValue == None):
                errorHandler(56, "", "", "assigning None to a variable")
            self.varExists(data)
            frame, name = self.getFrameAndName(data)

            specificFrame = self.getSpecificFrame(frame)
            variable = specificFrame[name]

            variable.type = newType
            variable.value = newValue
        except Exception as e:
            errorHandler(99, "frame.py", "assignVariable")

    def getSpecificFrame(self, frame: str):
        '''
        :param frame: string shortcut for specific frame (GF, LF, TF
        :return: specific frame for searching variables
        '''
        try:
            if (frame.upper() == "GF"):
                return self.globalFrame

            elif (frame.upper() == "LF"):
                return self.localFrame

            elif (frame.upper() == "TF"):
                return self.temporaryFrame
            else:
                errorHandler(55, "frame.py", "getSpecificFrame", "non-existing frame for variable")

        except Exception as e:
            errorHandler(99, "frame.py", "getSpecificFrame")

    def getFrameAndName(self, data):
        '''
        :param data: string 'GF|LF|TF@someName' or instance of Variable class
        :return: frame, name
        '''
        frame = None
        name = None
        try:
            if (isinstance(data, Variable)):
                frame = data.frame
                name = data.name
            else:
                frame, name = data.split("@") #data = (GF@name, LF@name2)

            return frame, name
        except Exception as e:
            errorHandler(99, "frame.py", "getFrameAndName")

    def varExists(self, data):
        '''
        :param data: string 'GF|LF|TF@someName' or instance of Variable class
        :return: true if exists, exit(54) if not
        '''
        frame, name = self.getFrameAndName(data)
        specificFrame = self.getSpecificFrame(frame)
        if(
                specificFrame == None or
                (frame == "TF" and self.temporaryFrameisDefined == False) or
                (frame == "LF" and self.localFrameisDefined == False)
        ):
            errorHandler(55, "", "", "non-existing frame: " + frame + "@" + name)

        if(name not in specificFrame):
            errorHandler(54, "", "", "non-existing variable: "+frame+"@"+name)

        return True

    def varExistsNoErrorHandeling(self, data):
        '''
        :param data: string 'GF|LF|TF@someName' or instance of Variable class
        :return: true if exists, exit(54) if not
        '''
        frame, name = self.getFrameAndName(data)
        specificFrame = self.getSpecificFrame(frame)
        if(specificFrame == None or (frame == "TF" and self.temporaryFrameisDefined == False)):
            return False

        if(name not in specificFrame):
            return False

        return True

    def getRidOfSpecialChars(self, string): #special char can be \065 and that's for letter A in utf8
        try:
            for match in re.finditer(r'\\[0-9]{3}', string):
                UNInumber = match.group()[1::]
                UNIchar = chr(int(UNInumber))
                string = string.replace(match.group(), UNIchar)
            return string
        except Exception as e:
            return None


    def getVar(self, data): #data = (GF@name, LF@name2) | variable object
        '''
        :param data: string 'GF|LF|TF@someName' or instance of Variable class
        :return: instance of Variable class
        '''
        self.varExists(data)
        frame, name = self.getFrameAndName(data)
        specificFrame = self.getSpecificFrame(frame)
        return specificFrame[name]

    def createFrame(self):
        # Create new temporary frame
        self.temporaryFrame = {}
        self.temporaryFrameisDefined = True


    def pushFrame(self):
        # Move temporary frame to frameStack
        if(self.temporaryFrameisDefined):
            self.frameStack.append(self.temporaryFrame)

            if(len(self.localFrame) != 0):
                self.localFrameStorage.append(self.localFrame)
            self.localFrame = self.frameStack[-1]
            self.localFrameisDefined = True

            self.temporaryFrame = None
            self.temporaryFrameisDefined = False
        else:
            errorHandler(55, "Frame.py", "pushFrame", "Attemp to undefined frame")

    def popFrame(self):
        # move local frame to temporary
        try:
            self.temporaryFrame = self.localFrame
            self.localFrame = self.localFrameStorage.pop()
            self.frameStack.pop()
            self.temporaryFrameisDefined = True
        except Exception as e:
            errorHandler(55, "Frame.py", "pushFrame", "No local frame")

    def printFrames(self):
        print("-----------------------------------------------------------------------------------")
        print("FRAMES")
        print("-----------------------------------------------------------------------------------")
        print("global Frame:")
        for globalFrame in self.globalFrame:
            print(globalFrame)
        print("-----------------------------------------------------")
        print("localFrame:")
        for localFrame in self.localFrame:
            print(localFrame)
        print("-----------------------------------------------------")
        print("temporaryFrame:")
        for temporaryFrame in self.temporaryFrame:
            print(temporaryFrame)
        print("-----------------------------------------------------")