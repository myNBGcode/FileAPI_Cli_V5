Keep in mind that, either the executed mass transfer file is in **CSV** or **XML** format, the corresponding **default** outcome file will always be in **JSON** format. For more outcome options please take a look [here](https://github.com/myNBGcode/FileAPI_Cli_V4_1/blob/a7d692971a844ff978d60afdc6630bb34d08b925/BasicInstructions.txt#L735).

Below the possible values of the **status** field for the XML and JSON formats of an **outcome file** are explained:

| **Value**  |  **Meaning**|
| ------------- | ------------- |
| 0  | Pending  |
| 1  | Completed  |
| 2  | Failed  |
| 3  | Cancelled by user  |
| 4  | Partially executed  |
