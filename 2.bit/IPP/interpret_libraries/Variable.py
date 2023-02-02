'''
* Project: interpret.py
* 2021/2022
* @author Marek Klofera <xklofe01@stud.fit.vutbr.cz> - VUT FIT student
'''

class Variable:
    def __init__(self, data):
        super().__init__()

        frame, name = data.split("@")
        self.name = name
        self.frame = frame.upper()

        self.type = None
        self.value = None
        self.valueSet = False #some **** can move None to var :)



