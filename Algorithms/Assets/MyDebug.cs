#define Debug
#define Release
#undef Debug
using UnityEngine;
using System.Collections;

public class MyDebug : MonoBehaviour {

    void Start()
    {
        #region  此处用于讲解#define Debug，#if  #elif #else  #endif  #warning  #error
#if Debug && Release == false
        print(Debug&&Release"不同时存在");
#elif Debug && Release
#error"同时存在Debug&&Release是不允许的"
#elif (!Debug) || Release
        print("只存在Release版本");
#else
        print("两个版本都不存在");
#endif
#warning "别忘了把下面的语句注释掉"
        print("I hate my Boss!");
#endregion
    }
}
