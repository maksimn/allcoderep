#include <iostream>
#include <vector>
#include <string>
#include <string_view>
#include <algorithm>

int findIndexFor(char ch) {
    return static_cast<int>(ch) - static_cast<int>('a');
}

int findZeroCount(const std::vector<int>& a) {
    int ans = 0;
    for (int v : a) {
        if (v == 0) {
            ans++;
        }
    }
    return ans;
}

int countIs(std::string_view s) {
    int count = 0;
    const int K = 26;

    std::vector<int> a(K, 0);
    std::vector<int> b(K, 0);

    for (char ch : s) {
        int ind = findIndexFor(ch);
        if (ind >= 0 && ind < K) {
            b[ind]++;
        }
    }

    for (char ch : s) {
        int ind = findIndexFor(ch);
        if (ind >= 0 && ind < K) {
            a[ind]++;
            b[ind]--;

            if (findZeroCount(a) == findZeroCount(b)) {
                count++;
            }
        }
    }

    return count;
}

std::pair<bool, int> isSymmetricWithCount(std::string_view s) {
    int count = countIs(s);
    return {count != 0, count};
}

void test_1() {
    std::string s = "aaa";
    int res = countIs(s);
    std::cout << "res=" << res << " for \"" << s << "\"" << std::endl;
}

void test_2() {
    std::string s = "abc";
    int res = countIs(s);
    std::cout << "res=" << res << " for \"" << s << "\"" << std::endl;
}

void entrance_task_2_12() {
    test_1();
    test_2();
}
