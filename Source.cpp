#include <iostream>
#include <vector>
using namespace std;

//Ex.1

template <typename T>
T Max(vector<T>& arr) {
    T max = arr[0];
    for (size_t i = 1; i < arr.size(); ++i) {
        if (arr[i] > max) {
            max = arr[i];
        }
    }
    return max;
}

template <typename T>
T Min(vector<T>& arr) {
    T min = arr[0];
    for (size_t i = 1; i < arr.size(); ++i) {
        if (arr[i] < min) {
            min = arr[i];
        }
    }
    return min;
}

template <typename T>
void Sort(vector<T>& arr) {
    for (size_t i = 0; i < arr.size(); ++i) {
        for (size_t j = i + 1; j < arr.size(); ++j) {
            if (arr[i] > arr[j]) {
                swap(arr[i], arr[j]);
            }
        }
    }
}

template <typename T>
bool Binary(vector<T>& arr, T value) {
    size_t left = 0;
    size_t right = arr.size() - 1;

    while (left <= right) {
        size_t mid = left + (right - left) / 2;

        if (arr[mid] == value) {
            return true;
        }

        if (arr[mid] < value) {
            left = mid + 1;
        }
        else {
            right = mid - 1;
        }
    }

    return false;
}

template <typename T>
void ReplaceArr(vector<T>& arr, T oldValue, T newValue) {
    for (size_t i = 0; i < arr.size(); ++i) {
        if (arr[i] == oldValue) {
            arr[i] = newValue;
        }
    }
}

//Ex.2

template <typename T>
class Matrix {
private:
    int rows, cols;
    T** data;

public:
    Matrix(int rows, int cols) : rows(rows), cols(cols) {
        data = new T* [rows];
        for (int i = 0; i < rows; i++) {
            data[i] = new T[cols];
        }
    }

    void Clear() {
        for (int i = 0; i < rows; i++) {
            delete[] data[i];
        }
        delete[] data;
    }

    void Keyboard() {
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                cin >> data[i][j];
            }
        }
    }

    void Randomly() {
        srand(time(0));
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                data[i][j] = rand() % 100;
            }
        }
    }

    void Display() const {
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                cout << data[i][j] << ' ';
            }
            cout << endl;
        }
    }

    Matrix<T> operator+(const Matrix<T>& other) const {
        Matrix<T> result(rows, cols);
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                result.data[i][j] = data[i][j] + other.data[i][j];
            }
        }
        return result;
    }

int main() {
    return 0;
}