using UnityEngine;
using System.Collections;
using System.Text;

public class Alphabet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public static Alphabet BINARY = new Alphabet("01");

    /**
     *  The octal alphabet { 0, 1, 2, 3, 4, 5, 6, 7 }.
     */
    public static Alphabet OCTAL = new Alphabet("01234567");

    /**
     *  The decimal alphabet { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.
     */
    public static Alphabet DECIMAL = new Alphabet("0123456789");

    /**
     *  The hexadecimal alphabet { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, A, B, C, D, E, F }.
     */
    public static Alphabet HEXADECIMAL = new Alphabet("0123456789ABCDEF");

    /**
     *  The DNA alphabet { A, C, T, G }.
     */
    public static Alphabet DNA = new Alphabet("ACGT");

    /**
     *  The lowercase alphabet { a, b, c, ..., z }.
     */
    public   Alphabet LOWERCASE = new Alphabet("abcdefghijklmnopqrstuvwxyz");

    /**
     *  The uppercase alphabet { A, B, C, ..., Z }.
     */

    public static  Alphabet UPPERCASE = new Alphabet("ABCDEFGHIJKLMNOPQRSTUVWXYZ");

    /**
     *  The protein alphabet { A, C, D, E, F, G, H, I, K, L, M, N, P, Q, R, S, T, V, W, Y }.
     */
    public static Alphabet PROTEIN = new Alphabet("ACDEFGHIKLMNPQRSTVWY");

    /**
     *  The base-64 alphabet (64 characters).
     */
    public static Alphabet BASE64 = new Alphabet("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/");

    /**
     *  The ASCII alphabet (0-127).
     */
    public static Alphabet ASCII = new Alphabet(128);

    /**
     *  The extended ASCII alphabet (0-255).
     */
    public static Alphabet EXTENDED_ASCII = new Alphabet(256);

    /**
     *  The Unicode 16 alphabet (0-65,535).
     */
    public static Alphabet UNICODE16      = new Alphabet(65536);


    private char[] alphabet;     // the characters in the alphabet
    private int[] inverse;       // indices
    public  int r;         // the radix of the alphabet

    /**
     * Initializes a new alphabet from the given set of characters.
     *
     * @param alpha the set of characters
     */
    public Alphabet(string alpha)
    {

        // check that alphabet contains no duplicate chars
        //bool[] unicode = new bool[Character.MAX_VALUE];
        bool[] unicode = new bool[char.MaxValue];
        for (int i = 0; i < alpha.Length; i++)
        {
            char c = alpha[i];
            if (unicode[c])
                throw new System.Exception("Illegal alphabet: repeated character = '" + c + "'");
            unicode[c] = true;
        }

        alphabet = alpha.ToCharArray();
        r = alpha.Length;
        inverse = new int[char.MaxValue];
        for (int i = 0; i < inverse.Length; i++)
            inverse[i] = -1;

        // can't use char since R can be as big as 65,536
        for (int c = 0; c < r; c++)
            inverse[alphabet[c]] = c;
    }

    /**
     * Initializes a new alphabet using characters 0 through R-1.
     *
     * @param radix the number of characters in the alphabet (the radix R)
     */
    public Alphabet(int radix)
    {
        this.r = radix;
        alphabet = new char[r];
        inverse = new int[r];

        // can't use char since R can be as big as 65,536
        for (int i = 0; i < r; i++)
            alphabet[i] = (char)i;
        for (int i = 0; i < r; i++)
            inverse[i] = i;
    }

    /**
     * Initializes a new alphabet using characters 0 through 255.
     */
    public Alphabet()
    {
        int radix=256;
        this.r = radix;
        alphabet = new char[r];
        inverse = new int[r];

        // can't use char since R can be as big as 65,536
        for (int i = 0; i < r; i++)
            alphabet[i] = (char)i;
        for (int i = 0; i < r; i++)
            inverse[i] = i;
    }

    /**
     * Returns true if the argument is a character in this alphabet.
     *
     * @param  c the character
     * @return {@code true} if {@code c} is a character in this alphabet;
     *         {@code false} otherwise
     */
    public bool contains(char c)
    {
        return inverse[c] != -1;
    }

    /**
     * Returns the number of characters in this alphabet (the radix).
     * 
     * @return the number of characters in this alphabet
     * @deprecated Replaced by {@link #radix()}.
     */
   
    public int R()
    {
        return r;
    }

    /**
     * Returns the number of characters in this alphabet (the radix).
     * 
     * @return the number of characters in this alphabet
     */
    public int radix()
    {
        return r;
    }

    /**
     * Returns the binary logarithm of the number of characters in this alphabet.
     * 
     * @return the binary logarithm (rounded up) of the number of characters in this alphabet
     */
    public int lgR()
    {
        int lgR = 0;
        for (int t = r - 1; t >= 1; t /= 2)
            lgR++;
        return lgR;
    }

    /**
     * Returns the index corresponding to the argument character.
     * 
     * @param  c the character
     * @return the index corresponding to the character {@code c}
     * @throws IllegalArgumentException unless {@code c} is a character in this alphabet
     */
    public int toIndex(char c)
    {
        if (c >= inverse.Length || inverse[c] == -1)
        {
            throw new System.Exception("Character " + c + " not in alphabet");
        }
        return inverse[c];
    }

    /**
     * Returns the indices corresponding to the argument characters.
     * 
     * @param  s the characters
     * @return the indices corresponding to the characters {@code s}
     * @throws IllegalArgumentException unless every character in {@code s}
     *         is a character in this alphabet
     */
    public int[] toIndices(string s)
    {
        char[] source = s.ToCharArray();
        int[] target = new int[s.Length];
        for (int i = 0; i < source.Length; i++)
            target[i] = toIndex(source[i]);
        return target;
    }

    /**
     * Returns the character corresponding to the argument index.
     * 
     * @param  index the index
     * @return the character corresponding to the index {@code index}
     * @throws IllegalArgumentException unless {@code 0 <= index < R}
     */
    public char toChar(int index)
    {
        if (index < 0 || index >= r)
        {
            throw new System.Exception("index must be between 0 and " + r + ": " + index);
        }
        return alphabet[index];
    }

    /**
     * Returns the characters corresponding to the argument indices.
     * 
     * @param  indices the indices
     * @return the characters corresponding to the indices {@code indices}
     * @throws IllegalArgumentException unless {@code 0 < indices[i] < R}
     *         for every {@code i}
     */
    public string toChars(int[] indices)
    {
        StringBuilder s = new StringBuilder(indices.Length);
        for (int i = 0; i < indices.Length; i++)
            s.Append(toChar(indices[i]));
        return s.ToString();
    }

    /**
     * Unit tests the {@code Alphabet} data type.
     *
     * @param args the command-line arguments
     */
    public static void main(string[] args)
    {
        int[] encoded1 = Alphabet.BASE64.toIndices("NowIsTheTimeForAllGoodMen");
        string decoded1 = Alphabet.BASE64.toChars(encoded1);
        print(decoded1);

        int[] encoded2 = Alphabet.DNA.toIndices("AACGAACGGTTTACCCCG");
        string decoded2 = Alphabet.DNA.toChars(encoded2);
        print(decoded2);

        int[] encoded3 = Alphabet.DECIMAL.toIndices("01234567890123456789");
        string decoded3 = Alphabet.DECIMAL.toChars(encoded3);
        print(decoded3);
    }
}
