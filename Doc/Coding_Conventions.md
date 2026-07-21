# C# 코딩 컨벤션 (C# Coding Conventions)

## 변수 (Variables)
* 엑세스 수준 한정자 (`public`, `private`, `protected`) 생략 금지
* 한 줄에 하나의 변수 선언
* 의미를 명확하게 작성
* 명사 사용 (`bool` 제외)
* `bool` 변수는 동사 접두사 사용 가능 (`isDead`, `hasDamageMultipiler` 등)
* `private`, `protected` 멤버 변수는 밑줄(`_`) 사용
  * 예) `int _elapsedTimeInDays;`
* `public` 변수는 파스칼 케이스(PascalCase) 사용
  * 예) `int ElapsedTimeInDays;`
* 정적 변수는 `s_` 접두사 사용
  * 예) `static object s_instance;`
* 지역 변수, 매개 변수는 접두사 없이 카멜 케이스(camelCase) 사용
  * 예) `int speed;`
* `public`, `private` 멤버 변수는 함께 그룹화

```csharp
// EXAMPLE: public and private variables
public float DamageMultiplier = 1.5f;
public float MaxHealth;
public bool IsInvincible;

private bool _isDead;
private float _currentHealth;

// parameters
public void InflictDamage(float damage, bool isSpecialDamage)
{
    // local variable
    int totalDamage = damage;

    // local variable versus public member variable
    if (isSpecialDamage)
    {
        totalDamage *= DamageMultiplier;
    }

    // local variable versus private member variable
    if (totalDamage > _currentHealth)
    {
        // ...
    }
}
```

* `var` 키워드는 의미가 명확하고 가독성에 도움이 될 때(긴 이름 유형)에만 사용

```csharp
// EXAMPLE: good use of var
var powerUps = new List<PowerUps>();
var dictionary = new Dictionary<string, List<GameObject>>();

// AVOID: potential ambiguity
var powerUps = PowerUpManager.GetPowerUps();
```

## 상수 (Constants)
* 모든 문자 대문자로 작성
* 스네이크 케이스(SNAKE_CASE) 사용
* 예) `public const string AVATAR_LIST_URL = "avatar";`

## 열거형 (Enums)
* 파스칼 케이스 사용
* 열거형 이름에 단수 명사 사용
* `System.FlagsAttribute`로 표시된 열거형은 복수형으로 사용

```csharp
// EXAMPLE: enums use singular nouns
public enum WeaponType
{
    Knife,
    Gun,
    RocketLauncher,
    BFG
}

public enum FireMode
{
    None = 0,
    Single = 5,
    Burst = 7,
    Auto = 8,
}

// EXAMPLE: but a bitwise enum is plural
[Flags]
public enum AttackModes
{
    // Decimal // Binary
    None = 0,               // 000000
    Melee = 1,              // 000001
    Ranged = 2,             // 000010
    Special = 4,            // 000100
    MeleeAndSpecial = Melee | Special // 000101
}
```

## 클래스, 인터페이스 (Classes, Interfaces)
* 파스칼 케이스 사용
  * 예) `public class ExampleClass`
* 인터페이스는 접두사 대문자 `I`를 붙인다.
  * 예) `public interface IKillable`

```csharp
// EXAMPLE: Class formatting
public class ExampleClass : MonoBehaviour
{
    public int PublicField;
    public static int MyStaticField;

    private int _packagePrivate;
    private int _myPrivate;

    private static int _sMyPrivate;

    protected int _myProtected;

    public void DoSomething()
    {

    }
}

// EXAMPLE: Interfaces
public interface IKillable
{
    void Kill();
}

public interface IDamageable<T>
{
    void Damage(T damageTaken);
}
```

## 메서드 (Methods)
* 동사로 시작
* 매개변수는 카멜 케이스 사용 (지역변수와 같은 방법)
  * 예) `int currentPosition`
* `bool`을 반환하는 메서드는 질문 형식 사용
  * 예) `IsGameOver`, `HasStartedTurn`

```csharp
// EXAMPLE: Methods start with a verb
public void SetInitialPosition(float x, float y, float z)
{
    transform.position = new Vector3(x, y, z);
}

// EXAMPLE: Methods ask a question when they return bool
public bool IsNewPosition(Vector3 currentPosition)
{
    return (transform.position == newPosition);
}
```

## 이벤트 (Events)
* 동사구가 포함된 이름 사용
* `before` : 현재분사, `after` : 과거분사

```csharp
// event before
public event Action OpeningDoor;

// event after
public event Action DoorOpened;
```

* 기본적으로 `System.Action<T>`를 사용한다.
* 이벤트 발생 메서드는 `On` 접두사를 붙인다.

```csharp
// raises the Event if you have subscribers
public void OnDoorOpened()
{
    DoorOpened?.Invoke();
}

public void OnPointsScored(int points)
{
    PointsScored?.Invoke(points);
}
```

* 이벤트 관찰자의 이름은 `[이벤트 실행 주체의 이름]_[이벤트이름]`으로 지정한다.

```csharp
public class GameEvent
{
    //event
    public event Action OpeningDoor;

    public void OnOpeningDoor()
    {
        OpeningDoor?.Invoke();
    }
}

public class Observer
{
    void Start()
    {
        GameEvent game = new GameEvent();
        game.OpeningDoor += GameEvent_OpeningDoor;
    }

    public void GameEvent_OpeningDoor()
    {
    }
}
```

## 네임스페이스 (Namespaces)
* 기본적으로는 사용하지 않는다.
* 구매한 에셋 또는 라이브러리와 클래스명이 충돌할 경우 사용한다.
* 특수 기호나 밑줄 없는 파스칼 케이스 사용
* 네임스페이스가 여러 번 나올 경우 반복 입력하지 않도록 상단에 `using` 행 추가

## 직렬화 (Serialization)
* Inspector에 표시되도록 하기 위해서 변수를 `public`으로 만들기 보다는 되도록 `[SerializeField]` 사용.

## 중괄호 들여쓰기 (Brace Indentation)
* **Allman style** - 여는 중괄호를 새 줄에 배치 (권장)
* **K&R style** - 여는 중괄호를 이전 헤더와 같은 줄에 배치 (지양)

```csharp
// EXAMPLE: Allman or BSD style puts opening brace on a new line. (권장)
void DisplayMouseCursor(bool showMouse)
{
    if (!showMouse)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    else
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

// EXAMPLE: K&R style puts opening brace on the previous line.
void DisplayMouseCursor(bool showMouse) {
    if (!showMouse) {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    else {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
```

* Allman style 사용
* 가능하면 한 줄 문장의 경우에도 중괄호를 생략하지 않는다.
* 중첩된 여러 줄에 중괄호를 생략하지 않는다.

```csharp
// EXAMPLE: keep braces for clarity
for (int i = 0; i < 10; i++)
{
    for (int j = 0; j < 10; j++)
    {
        ExampleAction();
    }
}

// AVOID: removing braces from nested multi-line statements
for (int i = 0; i < 10; i++)
    for (int j = 0; j < 10; j++)
        ExampleAction();
```

* `switch`문 들여쓰기

```csharp
// EXAMPLE: indent cases from the switch statement
switch (someExpression)
{
    case 0:
        DoSomething();
        break;
    case 1:
        DoSomethingElse();
        break;
    case 2:
        int n = 1;
        DoAnotherThing(n);
        break;
}
```

## 가로 간격 (Horizontal Spacing)
* 공백을 넣어서 코드 밀도 감소

```csharp
// EXAMPLE: add spaces to make lines easier to read
for (int i = 0; i < 100; i++) { DoSomething(i); }

// AVOID: no spaces
for(inti=0;i<100;i++){DoSomething(i);}
```

* 함수 인수 사이에 쉼표 뒤 공백을 넣어서 사용

```csharp
// EXAMPLE: single space after comma between arguments
CollectItem(myObject, 0, 1);

// AVOID:
CollectItem(myObject,0,1);
```

* 괄호와 함수 인수 뒤에 공백 없이 사용

```csharp
// EXAMPLE: no space after the parenthesis and function arguments
DropPowerUp(myPrefab, 0, 1);

//AVOID:
DropPowerUp( myPrefab, 0, 1 );
```

* 함수 이름과 괄호 사이에 공백 없이 사용

```csharp
// EXAMPLE: omit spaces between a function name and parenthesis.
DoSomething()

// AVOID
DoSomething ()
```

* 대괄호 안에 공백 X

```csharp
// EXAMPLE: omit spaces inside brackets
x = dataArray[index];

// AVOID
x = dataArray[ index ];
```

* 흐름 제어 조건 앞에 공백 사용
* 피연산자와 연산자 사이에 공백 사용

```csharp
// EXAMPLE: space before condition; separate parentheses with a space.
while (x == y)

// AVOID
while(x==y)
```

## 세로 간격 (Vertical Spacing)
* 비슷한 작업을 수행하는 메서드는 서로 가깝게 유지
* 수직 공백을 사용하여 변수, 메서드를 분리한다.
* 변수에서도 논리적으로 연관 있는 것끼리 분리한다.

## #region
* 논리적으로 세로 간격 분리가 잘되어 있다면 `region`은 필요 없다.

## Class 설계
* 가능한 크기를 작게 유지
* 하나의 클래스에 하나의 책임(기능) 부여
* 중복 코드 제거
* **형태(순서):**
  1. Fields
  2. Properties
  3. Event / Delegates
  4. Monobehaviour Methods (Awake, Start, OnEnable, OnDisable, OnDestory..)
  5. Public Methods
  6. Private Methods

## Method 설계
* 하나의 메서드에 하나의 책임 부여
* 메서드 이름은 의도, 목적을 명확히 함
* 인수는 가능한 적게 사용
* 플래그를 전달하지 말고 다른 메서드를 분리하여 만듦
  * `GetAngle(bool isDegree)` (X)
  * `GetAngleInDegree` (O)
  * `GetAngleInRadians` (O)
* **Extension Method**는 API를 확장하는 깔끔한 방법
  * UnityEngine API 또는 구매 에셋의 API를 확장하고 싶을 경우, Extension Method를 사용하면 원본은 건드리지 않고 만들 수 있다.
  * 예) `GameObject.GetOrAddComponent`

## 주석 (Comments)
* 가능하면 변수 이름, 함수 이름을 명확하게 하여 주석을 남기지 않도록 한다.
* 일반적이지 않은 상황에 대응하여 코드를 작성할 경우, 그 이유에 대해 주석을 달아둔다.
