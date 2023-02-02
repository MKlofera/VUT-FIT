'''
* Project: interpret.py
* interpreting IPPcode22
* 2021/2022
* @author Marek Klofera <xklofe01@stud.fit.vutbr.cz> - VUT FIT student
'''

from interpret_libraries.ErrorHandler import errorHandler
from interpret_libraries.Variable import Variable
from pprint import pprint
import inspect


class DataStack:

    def __init__(self):
        super().__init__()
        # in dataStack are variables (objects) and symbs (dict)
        self.dataStack = []

    def pushValue(self, arg, frame):
        # instruction attribute is a var
        if (arg["attribType"] == "VAR"):
            frame.varExists(arg["data"])
            variable = frame.getVar(arg["data"])
            self.dataStack.append(variable)
        # instruction attribute is a symb
        else:
            self.dataStack.append(arg)

    def popValue(self):
        popedValue = None
        try:
            popedValue = self.dataStack.pop()
        except:
            errorHandler(56, "DataStack.py", "pop", "Can't pop from data stack (probably emty)")

        return self.preparePopedValue(popedValue)

    def preparePopedValue(self, popedValue):
        if (isinstance(popedValue, Variable)):
            return popedValue.type, popedValue.value
        else:
            return popedValue["attribType"], popedValue["data"]

    def printStack(self):
        print("Data stack:")
        for element in self.dataStack:
            pprint(vars(element))
        print("--------------------------------------------------------------------------------------")