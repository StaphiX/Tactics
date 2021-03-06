using System;

public class TernaryTree
{
    private TernaryNode m_root = null;

    private void Add(string s, int pos, ref TernaryNode node)
    {
        if (node == null) { node = new TernaryNode(s[pos], false); }

        if (s[pos] < node.m_char) { Add(s, pos, ref node.m_left); }
        else if (s[pos] > node.m_char) { Add(s, pos, ref node.m_right); }
        else
        {
            if (pos + 1 == s.Length) { node.m_wordEnd = true; }
            else { Add(s, pos + 1, ref node.m_center); }
        }
    }

    public void Add(string s)
    {
        if (s == null || s == "") throw new ArgumentException();

        Add(s, 0, ref m_root);
    }

    public bool Contains(string s)
    {
        if (s == null || s == "") throw new ArgumentException();

        int pos = 0;
        TernaryNode node = m_root;
        while (node != null)
        {
            int cmp = s[pos] - node.m_char;
            if (s[pos] < node.m_char) { node = node.m_left; }
            else if (s[pos] > node.m_char) { node = node.m_right; }
            else
            {
                if (++pos == s.Length) return node.m_wordEnd;
                node = node.m_center;
            }
        }

        return false;
    }
}

class TernaryNode
{
    internal char m_char;
    internal TernaryNode m_left, m_center, m_right;
    internal bool m_wordEnd;

    public TernaryNode(char ch, bool wordEnd)
    {
        m_char = ch;
        m_wordEnd = wordEnd;
    }
}