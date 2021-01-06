using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    static Queue<ICommand> commandBuffer = new Queue<ICommand>();
    static List<ICommand> commandHistory = new List<ICommand>();

    static int counter;

    public static void AddCommand(ICommand command)
    {
        while(commandHistory.Count > counter)
        {
            commandHistory.RemoveAt(counter);
        }
        commandBuffer.Enqueue(command);
    }

    private void Update()
    {
        if (commandBuffer.Count > 0)
        {
            ICommand command = commandBuffer.Dequeue();
            command.Execute();

            commandHistory.Add(command);
            counter++;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                if(counter > 0)
                {
                    counter--;
                    commandHistory[counter].Undo();
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                if(counter < commandHistory.Count)
                {          
                    commandHistory[counter].Execute();
                    counter++;
                }
            }
        }
    }
}
