/* This file is not of my own creation, I found this code from a github thread linked bellow...
https://gist.github.com/LotteMakesStuff/c0a3b404524be57574ffa5f8270268ea

This file, along with ReadOnlyAttribute.cs simply allows me to have a read only attribute for variables 
visible in the inspector view in unity.
*/

// NOTE DONT put in an editor folder

using UnityEngine;
public class ReadOnlyAttribute : PropertyAttribute { }