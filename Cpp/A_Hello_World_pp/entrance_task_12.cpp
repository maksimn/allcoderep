#include <iostream>
#include <sstream>
#include <string>
#include <unordered_map>
#include <vector>

std::vector<int> find_sequence_count_after_operations(const std::vector<int>& a, int m) {
    // Резервируем вектор размером m, заполненный нулями
    std::vector<int> result(m, 0);
    std::unordered_map<int, int> dict;

    // Считаем частоту чисел
    for (size_t i = 0; i < a.size(); ++i) {
        dict[a[i]]++;
    }

    for (int j = 0; j < m; ++j) {
        int count = 0;

        // В C++11 используем итераторы или ссылку на std::pair
        for (auto it = dict.begin(); it != dict.end(); ++it) {
            // it->first — это ключ (x), it->second — это значение (q)
            if (it->second >= 2) {
                it->second -= 2;
            } else {
                it->second = 0;
            }
            count += it->second;
        }

        if (count == 0) break;

        result[j] = count;
    }

    return result;
}

std::vector<int> read_array_from(const std::string line) {
    std::istringstream iss(line);
    std::vector<int> nums;

    int temp;

    while (iss >> temp) {
        nums.push_back(temp);
    }

    return nums;
}

void task_12_test_1() {
    std::vector<int> v = {5, 5, 5};

    auto result = find_sequence_count_after_operations(v, 1);

    for (int n : result) {
        std::cout << n << " ";
    }

    std::cout << std::endl;
}

void task_12_test_2() {
    std::vector<int> v = {5, 5, 5};

    auto result = find_sequence_count_after_operations(v, 2);

    for (int n : result) {
        std::cout << n << " ";
    }

    std::cout << std::endl;
}

void entrance_task_12_tests() {
    task_12_test_1();
    task_12_test_2();
}

void entrance_task_12() {
    std::string line;

    std::getline(std::cin, line);

    std::vector<int> nums = read_array_from(line);

    int n = nums[0];
    int m = nums[1];

    std::getline(std::cin, line);

    nums = read_array_from(line);

    std::cout << "n=" << n << ", m=" << m << std::endl;

    for (int n : nums) {
        std::cout << n << " ";
    }

    std::cout << std::endl;

    std::vector<int> result = find_sequence_count_after_operations(nums, m);

    for (int n : result) {
        std::cout << n << " ";
    }

    std::cout << std::endl;
}
